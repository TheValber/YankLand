using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Handles spawning visitors at a house within randomised intervals.
/// Updates the UI to reflect the number of visitors spawned.
/// </summary>
public class House : MonoBehaviour
{
    // --- Spawn timing configuration ---
    float minSpawnTime = 5.0f; // Minimum time between visitor spawns
    float maxSpawnTime = 60.0f; // Maximum time between visitor spawns
    
    // --- Spawn timing state ---
    private float nextSpawnTime = 0.0f; // Time until the next visitor spawn
    private float spawnProgression = 0.0f; // Progression towards the next spawn
    
    // --- References ---
    [SerializeField] private GameObject visitor = null; // Prefab for the visitor to spawn
    private UIManager uiManager = null; // Reference to the UI manager for updating visitor count
    
    /// <summary>
    /// Initializes references and sets the initial spawn time.
    /// </summary>
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        nextSpawnTime = Random.Range(0, minSpawnTime);
    }

    /// <summary>
    /// Called once per frame.
    /// Handles progression towards spawning visitors and triggers the spawn when ready.
    /// </summary>
    void Update()
    {
        spawnProgression += Time.deltaTime;
        if (spawnProgression >= nextSpawnTime)
        {
            spawnProgression = 0.0f;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            Instantiate(visitor, transform.position, Quaternion.identity);
            uiManager.addVisitor(1);
        }
    }
}
