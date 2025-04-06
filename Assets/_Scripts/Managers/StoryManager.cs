using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private Image storyContainer;
    public void EndIntro()
    {
        storyContainer.gameObject.SetActive(false);
    }

    public void StormAudio()
    {
        StoryEvents.RaiseOnStorm(Camera.main.transform.position);
    }

    public void ThunderAudio()
    {
        StoryEvents.RaiseOnThunder(Camera.main.transform.position);
    }
}
