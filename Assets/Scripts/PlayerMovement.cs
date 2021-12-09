using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private LayerMask wallsLayerMask;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float velocity = 0.0f;
    [SerializeField] private float jumpHeight = 0.0f;
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
        if (!IsRightWall() || !IsLeftWall())
        {
            if(Input.GetKey (KeyCode.D))
            {
                this.rb.velocity = new Vector2(velocity, rb.velocity.y);
            }
            if(Input.GetKey (KeyCode.A))
            {
                this.rb.velocity = new Vector2(-velocity, rb.velocity.y);
            }
        }
        if (IsRightWall())
        {
            this.rb.velocity = new Vector2(-2, 0);
        }
        if (IsLeftWall())
        {
            this.rb.velocity = new Vector2(2, 0);
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

    private bool IsRightWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f,
            Vector2.right, .1f, wallsLayerMask);
       // Debug.DrawRay(boxCollider2D.bounds.center, Vector2.right * boxCollider2D.bounds.extents.y, Color.red);
        return raycastHit2D.collider != null;
    }

    private bool IsLeftWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f,
            Vector2.left, .1f, wallsLayerMask);
        //Debug.DrawRay(boxCollider2D.bounds.center, Vector2.left * boxCollider2D.bounds.extents.y, Color.green);
        return raycastHit2D.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectables"))
        {
            ScoreManager.instance.AddScore(10000);
            Debug.Log("Score");
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Enemies")){
           SceneManager.LoadScene(2);  //return to menu after player dies
        }
    }
}
