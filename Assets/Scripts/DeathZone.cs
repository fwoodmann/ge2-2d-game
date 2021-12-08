using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DeathZone : MonoBehaviour
{
    // Needs to move with camera/character
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject collectiblePrefab;
    private GameObject myPlat;

    public float levelWidth = 3.5f;
    public float minY = 1f;
    public float maxY = 1.75f;
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < 10; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            myPlat = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {

        myPlat = (GameObject)Instantiate(platformPrefab, 
        new Vector2(Random.Range(-levelWidth, levelWidth), myPlat.transform.position.y + (Random.Range(minY, maxY))), 
        Quaternion.identity);

        if(Random.Range(0, 10) >= 5)    //change propability of collectibles
        {
            Instantiate(collectiblePrefab, 
            new Vector2(myPlat.transform.position.x, myPlat.transform.position.y + 1), 
            Quaternion.identity);
        }

        Destroy(other.gameObject); // can remove platform objects to prevent lag
        //Debug.Log("Enter" + other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(2);  //return to menu after player dies
        }
         
    }
    
}
