using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VictoryUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDurationToWhite;
    [SerializeField] private float fadeDurationToClear;
    [SerializeField] private CinemachineCamera victoryCam;
    [SerializeField] private Image flashyImage;
    [SerializeField] private WakeupUI wakeupUI;
    [SerializeField] private Button resetButton;

    private void Start()
    {
        StoryEvents.OnGameWin += HandleGameWin;
        resetButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        flashyImage.enabled = true;
        wakeupUI.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void HandleGameWin()
    {
        Show();
        FadeIn();
    }

    private void ShowRestart()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        resetButton.gameObject.SetActive(true);
    }

    private IEnumerator FadeCanvasGroup(float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
        if (to == 1f && canvasGroup.alpha == to)
        {
            victoryCam.gameObject.SetActive(true);
            StartCoroutine(WaitSecondsCoroutine());
        }
        if (to == 0f && canvasGroup.alpha == to)
        {
            resetButton.gameObject.SetActive(true);
            flashyImage.enabled = false;
            canvasGroup.alpha = 1f;
        }
    }
    private void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(0f, 1f, fadeDurationToWhite));
    }

    IEnumerator WaitSecondsCoroutine()
    {
        yield return new WaitForSeconds(3);
        FadeOut();
        ShowRestart();
    }

    private void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(1f, 0f, fadeDurationToClear));
    }
}
