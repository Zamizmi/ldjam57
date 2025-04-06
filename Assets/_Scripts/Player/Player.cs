using System;
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
        Debug.Log("Respawn!");
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
        // Raycast from the center of the camera's viewport
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactionDistance, interactionsLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable interactable))
            {
                if (selectedInteractable != interactable)
                {
                    SetSelectedInteractable(interactable);
                }
            }
            else
            {
                SetSelectedInteractable(null);
            }
        }
        else
        {
            SetSelectedInteractable(null);
        }
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
