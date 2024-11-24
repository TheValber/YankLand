using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomly adjusts the rotation and scale of a tree object.
/// </summary>
public class RandomForestTree : MonoBehaviour
{
    // --- Congifuration for randomization ---
    private float minScale = 2.0f; // Minimum scale for the tree
    private float maxScale = 3.0f; // Maximum scale for the tree
    
    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    void Start()
    {
        // Randomize the rotation of the tree
        float randomRotationY = Random.Range(0f, 360f);
        transform.Rotate(0, randomRotationY, 0);
        
        // Randomize the scale of the tree
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}
