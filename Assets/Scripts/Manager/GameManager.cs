using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PasueGame()
    {
        // 일시정지 판넬 추가
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
