using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject bulletPrefab;

    void Update()
    {
        // Get mouse position using new Input System
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        Vector3 worldPosition = gameCamera.ScreenToWorldPoint(new Vector3(
            mousePosition.x, 
            mousePosition.y, 
            this.transform.position.z - gameCamera.transform.position.z
        ));

        this.transform.localEulerAngles = new Vector3(
            this.transform.localEulerAngles.x,
            this.transform.localEulerAngles.y,
            Mathf.Atan2(worldPosition.y - this.transform.position.y, worldPosition.x - this.transform.position.x) * Mathf.Rad2Deg
        );

        // Check for mouse click using new Input System
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = this.transform.position;
            
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.movementDirection = this.transform.right;
            }
        }
    }
}