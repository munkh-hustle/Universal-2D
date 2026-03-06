using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Update()
    {
        // Move downward
        transform.Translate(Vector2.down * Time.deltaTime * 2f);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // Destroy anything that hits the enemy
        // (except maybe other enemies)
        if (!c.CompareTag("Enemy"))
        {
            Destroy(c.gameObject); // Destroy whatever hit it
            Destroy(this.gameObject); // Destroy the enemy
        }
    }
}