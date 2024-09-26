using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
