using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score;
    public int highScore;

    public AudioSource PointSound;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        score = 0;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }
    }

    private void PlayPointSound()
    {
        PointSound.time = 0.2f;
        PointSound.Play();
    }

    public void AddScore()
    {
        PlayPointSound();

        score++;

        UIController.Instance.UpdateScore(score);

        if (score >= highScore) { 
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
