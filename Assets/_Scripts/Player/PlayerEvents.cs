using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<Vector3> OnFootStep;
    public static event Action<Vector3> OnJump;
    public static event Action<IInteractable> OnInteractableChange;
    public static event Action<IInteractable> OnInteraction;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerRespawn;

    public static void RaiseInteractableChanged(IInteractable changed)
    {
        OnInteractableChange?.Invoke(changed);
    }

    public static void RaiseOnInteraction(IInteractable changed)
    {
        OnInteraction?.Invoke(changed);
    }

    public static void RaiseOnFootStep(Vector3 position)
    {
        OnFootStep?.Invoke(position);
    }

    public static void RaiseOnFootJump(Vector3 position)
    {
        OnJump?.Invoke(position);
    }

    public static void RaiseOnDead()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void RaiseOnRespawn()
    {
        OnPlayerRespawn?.Invoke();
    }
}