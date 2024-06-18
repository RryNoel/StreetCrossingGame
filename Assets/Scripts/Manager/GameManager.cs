using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused;

    public bool IsPaused
    {
        get { return isPaused; }
    }

    public event Action<bool> OnPause;
    public event Action EndGame;

    [Header("일시정지")]
    public GameObject pausePanel;
    public Button pauseBtn;
    public Button playBtn;

    protected override void Awake()
    {
        base.Awake();
        EndGame += GameOver;
        OnPause += PauseGame;
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

    private void PauseGame(bool pause)
    {
        isPaused = pause;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
            pauseBtn.gameObject.SetActive(!isPaused);
            playBtn.gameObject.SetActive(isPaused);
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void GameOver()
    {
        // 게임오버 판넬 추가
    }
    public void PlayGame()
    {

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
