using TMPro;
using UnityEngine;

public class InteractHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactTextHolder;
    private IInteractable selectedInteractable;

    private void Start()
    {
        PlayerEvents.OnInteractableChange += HandleInteractableChange;
        PlayerEvents.OnInteraction += HandleInteractableChange;
    }

    private void OnDestroy()
    {
        PlayerEvents.OnInteractableChange -= HandleInteractableChange;
        PlayerEvents.OnInteraction -= HandleInteractableChange;
    }

    private void HandleInteractableChange(IInteractable changed)
    {
        selectedInteractable = changed;
        UpdateVisual();
    }

    private void ShowInteractText()
    {
        gameObject.SetActive(true);
    }

    private void HideInteractText()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        if (selectedInteractable != null)
        {
            string interactText = selectedInteractable.GetInteractText(Player.Instance);
            if (interactText != null)
            {
                interactTextHolder.text = "[E] " + interactText;
                ShowInteractText();
            }
            else
            {
                HideInteractText();
            }
        }
        else
        {
            HideInteractText();
        }
    }
}
