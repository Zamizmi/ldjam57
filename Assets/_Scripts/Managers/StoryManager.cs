using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private Image storyContainer;
    [SerializeField] private Animator animator;

    public void StartIntro()
    {
        animator.SetTrigger("Intro");
    }
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
