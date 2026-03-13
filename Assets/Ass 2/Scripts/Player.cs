using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movingVelocity = 2f;      // Forward speed
    public float flapForce = 5f;            // Upward force when tapping
    private Rigidbody2D rb;
    private bool isAlive = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on Player!");
        }
        else
        {
            // Set gravity to normal positive value (downward)
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
    }
    
    void Update()
    {
        if (!isAlive || rb == null) return;
        
        // Constant forward movement
        rb.linearVelocity = new Vector2(
            movingVelocity,
            rb.linearVelocity.y
        );
        
        // Flap when space is pressed
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }
    }
    
    void Flap()
    {
        // Apply upward force (reset vertical velocity first for consistent flapping)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
        Debug.Log("Flap! Velocity: " + rb.linearVelocity.y);
    }
    
    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.CompareTag("Obstacle") && isAlive)
        {
            Debug.Log("Player hit: " + c.gameObject.tag);
            isAlive = false;
            Destroy(this.gameObject);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}