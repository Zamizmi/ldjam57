using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private StoryManager storyManager;

    private void Start()
    {
        startButton.onClick.AddListener(() => StartGame());
    }

    private void StartGame()
    {
        storyManager.StartIntro();
        Cursor.visible = false;
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
