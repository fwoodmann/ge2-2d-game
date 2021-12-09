using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float velocity = 0.0f;
    [SerializeField] private float jumpHeight = 0.0f;
    [SerializeField] private int collectableValue = 0;
    [SerializeField] private BoxCollider2D boxCollider2D;
    Camera m_Camera;
    Vector3 cameraPos;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public AudioSource Sound;
    void Start()
    {
         m_Camera = Camera.main;
    }
    void Update()
    {
        if(IsGrounded())
        {
            Sound.volume = PlayerPrefs.GetFloat("volume", 0.1f);
            Sound.Play();
            this.rb.velocity = new Vector2(0, jumpHeight);
        }
    
        if(Input.GetKey (KeyCode.D))    //movement key right
        {
            this.rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        if(Input.GetKey (KeyCode.A))    //movement key left
        {
            this.rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        
        if (rb.position.x > 4.8f)   //Edge Case when player leaves to the right
        {
            rb.transform.position = new Vector2(-4.75f, rb.position.y); 
        }
        if (rb.position.x < -4.8f)  //Edge Case when player leaves to the left
        {
            rb.transform.position = new Vector2(4.75f, rb.position.y); 
        }
   
    }
    void LateUpdate()
    {
            Vector3 desiredPosition = new Vector3(0f, rb.position.y, -10f) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(m_Camera.transform.position, desiredPosition, smoothSpeed);
            if(desiredPosition.y > smoothedPosition.y)
            {
                m_Camera.transform.position = new Vector3(0, smoothedPosition.y, -10f);
            }
    }
      
    
    /*
     * https://www.youtube.com/watch?v=ptvK4Fp5vRY
     */
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f,
            Vector2.down, .1f, platformsLayerMask);
        //Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * boxCollider2D.bounds.extents.y, Color.red);
        return raycastHit2D.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectables"))
        {
            ScoreManager.instance.AddScore(collectableValue);
            //Debug.Log("Score");
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Enemies")){
           SceneManager.LoadScene(2);  //return to menu after player dies
        }
    }
}
