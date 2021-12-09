using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Sound;

    public void PlaySound()
    {
        Sound.volume = PlayerPrefs.GetFloat("volume", 0.1f);
        Sound.Play();
    }

    
}
