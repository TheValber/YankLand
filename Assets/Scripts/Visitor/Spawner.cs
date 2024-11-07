using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject visitor = null;
    private float spawnRadius = 6.0f;
    
    private UIManager uiManager = null;
    
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        // Spawn 1
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
            Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
            uiManager.addVisitor(1);
        }
        // Spawn 10
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
                Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
            }
            uiManager.addVisitor(10);
        }
        // Spawn 100
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < 100; i++)
            {
                Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
                Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
            }
            uiManager.addVisitor(100);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
