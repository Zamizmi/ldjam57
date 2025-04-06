using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VictoryUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;
    [SerializeField] private CinemachineCamera victoryCam;
    [SerializeField] private Image flashyImage;
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
        gameObject.SetActive(true);
    }

    private void HandleGameWin()
    {
        Show();
        victoryCam.gameObject.SetActive(true);
    }

    private void ShowRestart()
    {
        resetButton.gameObject.SetActive(true);
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
        if (to == 0f)
        {
            ShowRestart();
            flashyImage.enabled = false;
        }
        canvasGroup.alpha = to;
    }
    private void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(0f, 1f));
    }

    private void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(1f, 0f));
    }
}
