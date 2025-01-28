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

    public float speedPipes;
    public float numberPipes;
    public float distanceBetweenPipes;

    public Pipe pipePrefab;

    public GameState CurrentState;

    public Transform pipeSpawnPoint;

    void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        CurrentState = GameState.MainMenu;

        for (int i = 0; i < numberPipes; i++) { 
            Pipe pipe = Instantiate(pipePrefab, pipeSpawnPoint.position + new Vector3(i * distanceBetweenPipes, 0, 0), Quaternion.identity);
        }
    }

    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        CameraController.instance.Shake(0.45f, 0.45f);
        OnGameEnded?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
