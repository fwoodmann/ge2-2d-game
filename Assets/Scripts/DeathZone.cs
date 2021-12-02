using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DeathZone : MonoBehaviour
{
    // Needs to move with camera/character

    private void OnTriggerEnter2D(Collider2D other) 
    {
      
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(0);  //return to menu after player dies
        }
         
        Destroy(other.gameObject); // can remove platform objects to prevent lag
    }

}
