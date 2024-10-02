using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{
    private float speed = 15.0f;
    private float sprintSpeed = 30.0f;
    private float mouseSenstivity = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotate();
    }

    void move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        
        if (Input.GetKey(KeyCode.Space))
        {
            direction.y = 1.0f;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            direction.y = -1.0f;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * sprintSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
    }
    
    void rotate()
    {
        Vector2 mouseDelta = mouseSenstivity * new Vector2( Input.GetAxis( "Mouse X" ), -Input.GetAxis( "Mouse Y" ) );
        Quaternion rotation = transform.rotation;
        Quaternion horizontal = Quaternion.AngleAxis( mouseDelta.x, Vector3.up );
        Quaternion vertical = Quaternion.AngleAxis( mouseDelta.y, Vector3.right );
        transform.rotation = horizontal * rotation * vertical;
    }
}
