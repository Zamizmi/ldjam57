using System;
using UnityEngine;

public static class InteractableEvents
{
    public static event Action<Vector3> OnLeverTurn;
    public static event Action<Vector3> OnLanternTurn;

    public static void RaiseOnLeverTurn(Vector3 position)
    {
        OnLeverTurn?.Invoke(position);
    }

    public static void RaiseOnLanternlit(Vector3 position)
    {
        OnLanternTurn?.Invoke(position);
    }
}