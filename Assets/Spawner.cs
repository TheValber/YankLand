using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject visitor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn 1
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(visitor, transform.position, Quaternion.identity);
        }
        // Spawn 10
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(visitor, transform.position, Quaternion.identity);
            }
        }
        // Spawn 100
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < 100; i++)
            {
                Instantiate(visitor, transform.position, Quaternion.identity);
            }
        }
    }
}
