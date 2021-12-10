using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioSource Sound;
    // Start is called before the first frame update
    void Start()
    {
        Sound.volume = PlayerPrefs.GetFloat("volume");
        Sound.playOnAwake = true;
        Sound.Play();
    }
}
