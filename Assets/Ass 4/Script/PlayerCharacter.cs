using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float movementSpeed;
    public MapLimits Limits;
    
    void Update()
    {
        Movement();
        
        // Clamp position within boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, Limits.minimumX, Limits.maximumX),
            Mathf.Clamp(transform.position.y, Limits.minimumY, Limits.maximumY),
            0.0f
        );
    }

    void Movement()
{
    if (Input.GetKey(KeyCode.A))
    {
        transform.Translate(Vector3.right * -movementSpeed * Time.deltaTime);
    }
    if (Input.GetKey(KeyCode.D))
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }
    if (Input.GetKey(KeyCode.W))
    {
        transform.Translate(Vector3.up * -movementSpeed * Time.deltaTime);
    }
    if (Input.GetKey(KeyCode.S))
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
    }
}
}