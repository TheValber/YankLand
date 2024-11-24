using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

/// <summary>
/// Camera controller script for handling movement and rotation.
/// Includes normal movement, sprinting, and looking around.
/// </summary>
public class CameraController : MonoBehaviour
{
    // --- Movement configuration ---
    private float speed = 15.0f; // Movement speed
    private float sprintSpeed = 30.0f; // Movement speed while sprinting
    private float mouseSenstivity = 1.0f; // Mouse sensivity for camera rotation
    
    /// <summary>
    /// Called before the first frame update.
    /// Locks the mouse cursor at the center of the screen.
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Called once per frame.
    /// Handles camera movement and rotation.
    /// </summary>
    void Update()
    {
        move();
        rotate();
    }

    /// <summary>
    /// Handles camera movement based on user input.
    /// Supports horizontal and vertical movement, as well as upwards and downwards movement.
    /// </summary>
    void move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        
        // Handle vertical movement
        if (Input.GetKey(KeyCode.Space))
        {
            direction.y = 1.0f;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            direction.y = -1.0f;
        }
        
        // Use sprint speed if left shift is pressed, otherwise use normal speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * sprintSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
    }
    
    /// <summary>
    /// Handles camera rotation based on mouse movement.
    /// </summary>
    void rotate()
    {
        // Calculate mouse delta scaled by sensitivity
        Vector2 mouseDelta = mouseSenstivity * new Vector2( Input.GetAxis( "Mouse X" ), -Input.GetAxis( "Mouse Y" ) );
        
        // Compute horizontal and vertical rotations
        Quaternion rotation = transform.rotation;
        Quaternion horizontal = Quaternion.AngleAxis( mouseDelta.x, Vector3.up );
        Quaternion vertical = Quaternion.AngleAxis( mouseDelta.y, Vector3.right );
        
        // Apply rotations to camera
        transform.rotation = horizontal * rotation * vertical;
    }
}
