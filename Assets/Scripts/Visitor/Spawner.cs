using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Handles spawning visitors in a circular area.
/// Supports spawning 1, 10, or 100 visitors at a time.
/// </summary>
public class Spawner : MonoBehaviour
{
    // --- Configuration ---
    private float spawnRadius = 6.0f; // Radius of the spawn area
    
    // --- References ---
    [SerializeField] private GameObject visitor = null; // Prefab for the visitor to spawn
    private UIManager uiManager = null; // Reference to the UI manager for updating visitor count
    
    /// <summary>
    /// Initializes references to UI manager.
    /// </summary>
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    /// <summary>
    /// Handles spawning visitors based on user input.
    /// </summary>
    void Update()
    {
        // Spawn 1 visitor when E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnVisitors(1);
        }
        // Spawn 10 visitors when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnVisitors(10);
        }
        // Spawn 25 visitors when T is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnVisitors(25);
        }
    }
    
    /// <summary>
    /// Spawns the specified number of visitors within the spawn area.
    /// </summary>
    /// <param name="count">The number of visitors to spawn.</param>
    private void SpawnVisitors(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
            Instantiate(visitor, transform.position + new Vector3(spawnCircle.x, 0, spawnCircle.y), Quaternion.identity);
        }
        uiManager.addVisitor(count);
    }

    /// <summary>
    /// Draws a wireframe sphere around the spawn area for visualization.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
