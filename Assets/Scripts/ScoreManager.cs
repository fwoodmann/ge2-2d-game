using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /* This Class is inspired by this YT-Video https://www.youtube.com/watch?v=0zrZZN-QaDk */
    
    public static ScoreManager instance;
    public Text scoreText;
    public Text highScoreText;
    public int highScore = 0;
    public int score = 0;

    public void Awake()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt("HighScore");
        ResetScore();
    }

    public void AddScore(int points)
    {
        score = score + points;
        UpdateHighScore();
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("RunScore",score);
    }

    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }

        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("HighScore",highScore);
    }

    public void ResetScore()
    {
        Debug.Log("Reset");
        score = 0;
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        PlayerPrefs.DeleteKey("RunScore");
    }

    public void ClearHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
