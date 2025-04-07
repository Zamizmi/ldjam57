
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private IInteractable selectedInteractable;
    [SerializeField] private GameInput gameInput;
    [Header("Interactions")]
    [SerializeField] private float interactionDistance = 1.5f;
    [SerializeField] private LayerMask interactionsLayerMask;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cameraShake;
    private FirstPersonMovement fpsHandler;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractHandler += PlayerInteractHandler;
        PlayerEvents.OnPlayerRespawn += Respawn;
        fpsHandler = GetComponent<FirstPersonMovement>();
    }

    public void CameraShake(float duration)
    {
        cameraShake.AmplitudeGain = .1f;
        StartCoroutine(CameraShakeCoroutine(duration));
    }
    IEnumerator CameraShakeCoroutine(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            cameraShake.AmplitudeGain = Mathf.Lerp(1f, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lethal")
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        DisableLooking();
        PlayerEvents.RaiseOnDead();
    }

    private void Respawn()
    {
        transform.position = startingPoint.position;
        EnableLooking();
    }

    private void OnDisable()
    {
        gameInput.OnInteractHandler -= PlayerInteractHandler;
    }

    private void Update()
    {
        HandleInteractions();
    }

    public void DisableLooking()
    {
        fpsHandler.DisableMovement();
    }

    public void EnableLooking()
    {
        fpsHandler.EnableMovement();
    }

    private void HandleInteractions()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, interactionDistance, interactionsLayerMask);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteractable interactable))
            {
                SetSelectedInteractable(interactable);
                return;
            }
        }
        SetSelectedInteractable(null);

    }
    private void SetSelectedInteractable(IInteractable selectedInteractable)
    {
        this.selectedInteractable = selectedInteractable;
        PlayerEvents.RaiseInteractableChanged(selectedInteractable);
    }

    private void PlayerInteractHandler()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable?.Interact(this);
            PlayerEvents.RaiseOnInteraction(selectedInteractable);
        }
    }
}
