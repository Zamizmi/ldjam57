using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WakeupUI : MonoBehaviour
{
    [SerializeField] private bool isPlayerAwake = false;
    [SerializeField] private Image blockingImage;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;

    private void Start()
    {
        GameInput.Instance.OnInteractHandler += HandleInteraction;
        PlayerEvents.OnPlayerDeath += FadeIn;
        canvasGroup.alpha = 0f;
    }

    private void FadeIn()
    {
        isPlayerAwake = false;
        StartCoroutine(FadeCanvasGroup(0f, 1f));
    }

    private void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(1f, 0f));
    }

    private void HandleInteraction()
    {
        if (isPlayerAwake) return;
        isPlayerAwake = true;
        PlayerEvents.RaiseOnRespawn();
        FadeOut();
    }

    private IEnumerator FadeCanvasGroup(float from, float to)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
