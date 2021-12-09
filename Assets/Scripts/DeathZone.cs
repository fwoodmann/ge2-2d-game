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
    public GameObject enemyPrefab;
    private GameObject myPlat;
    [SerializeField] private int platformValue = 0;
    [SerializeField] private int enemyProb = 0;
    [SerializeField] private int collectablesProb = 0;

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
        
        //collectible spawner
        if(Random.Range(0, 100) <= collectablesProb)    //change probability of collectibles
        {
            Instantiate(collectiblePrefab, 
            new Vector2(myPlat.transform.position.x, myPlat.transform.position.y + 0.5f), 
            Quaternion.identity);
        }
        //enemy Spawner
        if(Random.Range(0, 100) <= enemyProb)    //change propability of collectibles
        {
            Instantiate(enemyPrefab, 
                new Vector2(0, myPlat.transform.position.y + 1), 
                Quaternion.identity);
        }

        if (other.CompareTag("Platforms"))
        {
            ScoreManager.instance.AddScore(platformValue);
        }
        

        Destroy(other.gameObject); // can remove platform objects to prevent lag
        //Debug.Log("Enter" + other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(2);  //return to menu after player dies
        }
        

    }
    
}
