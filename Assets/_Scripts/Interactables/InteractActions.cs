using System;
using System.Collections.Generic;

public static class InteractActions
{
    public static event Action<List<string>> OnLeverActivated;

    public static void OnLevelPulled(List<string> eventIds)
    {
        OnLeverActivated?.Invoke(eventIds);
    }
}