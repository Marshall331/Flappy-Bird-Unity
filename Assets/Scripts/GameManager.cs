using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    static public event Action OnGameStarted;
    static public event Action OnGameEnded;

    public enum GameState
    {
        MainMenu,
        InGame,
        GameOver
    }

    public GameState CurrentState;

    void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        OnGameEnded?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
