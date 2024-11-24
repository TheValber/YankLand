using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a list of Points of Interest (POI) in the scene.
/// Allows retrieving a random POI from the list.
/// </summary>
public class POISManager : MonoBehaviour
{
    // --- State ---
    public List<GameObject> pois = new List<GameObject>(); // List of all POIs in the scene
    
    /// <summary>
    /// Initializes the list of POIs with all the POIs in the scene.
    /// </summary>
    void Start()
    {
        pois.AddRange(GameObject.FindGameObjectsWithTag("POI"));
    }
    
    /// <summary>
    /// Retrieves a random POI that is different from the specified one.
    /// </summary>
    /// <param name="differentFrom">The POI to exclude from the selection.</param>
    /// <returns>A random POI that is different from the specified one.</returns>
    public GameObject GetRandomPOI(POI differentFrom)
    {
        if (pois.Count == 0)
        {
            return null; // Return null if no POIs are available
        }
        
        GameObject poi = pois[Random.Range(0, pois.Count)];
        // Ensure the selected POI is different from the specified one
        while (poi.GetComponent<POI>() == differentFrom)
        {
            poi = pois[Random.Range(0, pois.Count)];
        }
        return poi;
    }
}
