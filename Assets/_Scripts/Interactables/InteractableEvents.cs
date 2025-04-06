using System;
using UnityEngine;

public static class InteractableEvents
{
    public static event Action<Vector3> OnLeverTurn;
    public static event Action<Vector3> OnLanternTurn;
    public static event Action<Vector3> OnBallistaShot;
    public static event Action<Vector3> OnTargetActivation;

    public static void RaiseOnLeverTurn(Vector3 position)
    {
        OnLeverTurn?.Invoke(position);
    }

    public static void RaiseOnLanternlit(Vector3 position)
    {
        OnLanternTurn?.Invoke(position);
    }

    public static void RaiseOnBallistaShot(Vector3 position)
    {
        OnBallistaShot?.Invoke(position);
    }

    public static void RaiseOnTargetActivation(Vector3 position)
    {
        OnTargetActivation?.Invoke(position);
    }
}