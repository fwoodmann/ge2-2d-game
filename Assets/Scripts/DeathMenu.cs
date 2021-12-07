using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DeathMenu : MonoBehaviour
{
    public Text RunScoreText;
    public Text highScoreText;
    public void Awake()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        RunScoreText.text = PlayerPrefs.GetInt("RunScore").ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
