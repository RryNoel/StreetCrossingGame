using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action<bool> OnPause;
    public event Action EndGame;

    [Header("일시정지")]
    private bool isPaused;
    public GameObject pausePanel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        EndGame += GameOver;
        OnPause += PasueGame;
    }

    public void OnGameOverEvent()
    {
        EndGame?.Invoke();
    }

    public void OnPauseEvent(bool pause)
    {
        OnPause?.Invoke(pause);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ReturnGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public bool IsPaused
    {
        get { return isPaused; }
    }

    private void PasueGame(bool pause)
    {
        isPaused = pause;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void PlayGame()
    {

    }

    public void GameOver()
    {
        // 게임오버 판넬 추가
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
