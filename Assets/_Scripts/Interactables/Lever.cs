using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private List<string> actionIdsToSend;
    [SerializeField] private Animator leverAnimator;
    private string ANIM_IS_USED = "IsActive";

    private bool isUsed = false;

    public void Interact(Player player)
    {
        if (isUsed) return;
        isUsed = true;
        InteractableEvents.RaiseOnLeverTurn(transform.position);
        InteractActions.OnLevelPulled(actionIdsToSend);
        leverAnimator.SetBool(ANIM_IS_USED, isUsed);
    }

    public string GetInteractText(Player player)
    {
        return "Turn lever";
    }
}