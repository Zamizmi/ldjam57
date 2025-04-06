using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<Vector3> OnFootStep;
    public static event Action<Vector3> OnJump;

    public static void RaiseOnFootStep(Vector3 position)
    {
        OnFootStep?.Invoke(position);
    }

    public static void RaiseOnFootJump(Vector3 position)
    {
        OnJump?.Invoke(position);
    }
}