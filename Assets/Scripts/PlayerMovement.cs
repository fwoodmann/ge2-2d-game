using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float velocity = 0.0f;
    [SerializeField] private float jumpHeight = 0.0f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    Camera m_Camera;
    Vector3 cameraPos;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    void Start()
    {
         m_Camera = Camera.main;
    }
    void Update()
    {
        if(IsGrounded())
        {
            this.rb.velocity = new Vector2(0, jumpHeight);
        }
        if(Input.GetKey (KeyCode.D))
        {
            this.rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        if(Input.GetKey (KeyCode.A))
        {
            this.rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
   
    }
    void LateUpdate()
    {
            Vector3 desiredPosition = new Vector3(0f, rb.position.y+1.25f, -10f) + offset;
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
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * boxCollider2D.bounds.extents.y, Color.red);
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
    }
}
