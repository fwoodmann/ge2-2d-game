using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{    
    public Slider volumeSlider;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void updateVolume()
    {
       // Debug.Log(volumeSlider.value);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
