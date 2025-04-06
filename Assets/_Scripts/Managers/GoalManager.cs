using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.DisableLooking();
            OnVictory();
        }
    }
    private void OnVictory()
    {
        StoryEvents.RaiseOnVictory();
    }
}
