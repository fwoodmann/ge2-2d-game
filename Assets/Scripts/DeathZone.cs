using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DeathZone : MonoBehaviour
{
    // Needs to move with camera/character
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Destroy(other.gameObject); // can remove platform objects to prevent lag
        Debug.Log("Enter" + other.gameObject.name);

        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(0);  //return to menu after player dies
        }
         
    }
    
}
