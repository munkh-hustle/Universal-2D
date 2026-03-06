using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed;
    public Vector2 movementDirection;
    public float lifetime = 2f;

    void Start()
    {
        // Make sure bullet has Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet needs a Rigidbody2D component!");
        }
    }

    void Update()
    {
        // Apply movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = movementDirection * movementSpeed; // Changed here
        }
        
        // Destroy after lifetime
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}