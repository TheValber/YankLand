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
            // Vector2 spawnCircle = Random.insideUnitCircle * 50.0f;
            // Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
            Instantiate(visitor, transform.position, Quaternion.identity);
        }
        // Spawn 10
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 10; i++)
            {
                // Vector2 spawnCircle = Random.insideUnitCircle * 50.0f;
                // Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
                Instantiate(visitor, transform.position, Quaternion.identity);
            }
        }
        // Spawn 100
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < 100; i++)
            {
                // Vector2 spawnCircle = Random.insideUnitCircle * 50.0f;
                // Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
                Instantiate(visitor, transform.position, Quaternion.identity);
            }
        }
    }
}
