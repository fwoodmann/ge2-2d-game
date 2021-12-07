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
    void Start()
    {
         m_Camera = Camera.main;
         m_Camera.enabled = true;
    }
    void Update()
    {
        if(IsGrounded())
        {
            this.rb.velocity = new Vector2(0, jumpHeight);
            //Debug.Log(rb.position.y);
            m_Camera.transform.position = new Vector3(0, rb.position.y + 4, -10);
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
}
