using System;
using UnityEngine;

public static class StoryEvents
{
    public static event Action<Vector3> OnThunder;
    public static event Action<Vector3> OnStorm;
    public static event Action OnGameWin;

    public static void RaiseOnStorm(Vector3 position)
    {
        OnStorm?.Invoke(position);
    }

    public static void RaiseOnThunder(Vector3 position)
    {
        OnThunder?.Invoke(position);
    }

    public static void RaiseOnVictory()
    {
        OnGameWin?.Invoke();
    }
}