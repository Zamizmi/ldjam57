using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IInteractable selectedInteractable;
    public event Action<IInteractable> OnSelectedInteractableChanged;

    [SerializeField] private GameInput gameInput;
    [Header("Interactions")]
    [SerializeField] private float interactionDistance = 1.5f;
    [SerializeField] private LayerMask interactionsLayerMask;
    private void Start()
    {
        gameInput.OnInteractHandler += PlayerInteractHandler;
    }

    private void OnDisable()
    {
        gameInput.OnInteractHandler -= PlayerInteractHandler;
    }

    private void Update()
    {
        HandleInteractions();
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
        OnSelectedInteractableChanged?.Invoke(selectedInteractable);
    }

    private void PlayerInteractHandler()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable?.Interact(this);
        }
    }
}
