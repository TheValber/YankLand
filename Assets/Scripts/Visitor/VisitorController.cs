using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class VisitorController : MonoBehaviour
{
    // --- References ---
    private NavMeshAgent agent = null; // The NavMeshAgent that will move the visitor
    private Renderer visitorRenderer = null; // Used to hide the visitor when inside a POI
    private Collider visitorCollider = null; // Used to disable the collider when inside a POI
    
    private POISManager poisManager = null; // Used to get the POIs
    private UIManager uiManager = null; // Manages UI updates
    
    // --- Visitor State ---
    public Vector3 spawnPosition = Vector3.zero; // The spawn position of the visitor
    public int nbRemainingPOIs = 0;  // Number of POIs the visitor has to visit before leaving
    private bool isExiting = false; // Flag to check if the visitor is leaving
    
    private POI targetPOI = null; // Current target POI for the visitor
    private bool isInPOI = false; // Flag to check if the visitor is inside a POI
    
    /// <summary>
    /// Initialize the visitor and sets its first destination
    /// </summary>
    void Start()
    {
        // Initialize the components
        agent = GetComponent<NavMeshAgent>();
        visitorRenderer = GetComponent<Renderer>();
        visitorCollider = GetComponent<Collider>();
        
        poisManager = GameObject.Find("POIs").GetComponent<POISManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        
        spawnPosition = transform.position; // Save the spawn position
        nbRemainingPOIs = Random.Range(1, 5); // Randomly decide the number of POIs to visit

        // Choose a random POI as the first destination
        while (targetPOI == null)
        {
            targetPOI = poisManager.GetRandomPOI(null).GetComponent<POI>();
        }
        agent.SetDestination(targetPOI.GetInPoint());
        nbRemainingPOIs--;
    }

    /// <summary>
    /// Updates the visitor's movement and behavior each frame
    /// </summary>
    void Update()
    {
        // If the visitor is exiting, check if it has reached the spawn position to be destroyed
        if (isExiting)
        {
            if ((transform.position - spawnPosition).magnitude < 1.0f)
            {
                uiManager.removeVisitor(1);
                Destroy(gameObject);
            }
        }
        else if (!isInPOI && agent.hasPath && !agent.pathPending)
        {
            // Check if the visitor has reached the destination POI
            if (agent.remainingDistance < 10.0f && (agent.destination - transform.position).magnitude < 10.0f)
            {
                targetPOI.goInQueue(this);
                isInPOI = true;
                agent.radius = 0.6f;
                agent.avoidancePriority = 75;
            }
        }
    }
    
    /// <summary>
    /// Sets the visitor's priority for collision avoidance
    /// </summary>
    /// <param name="priority">The priority value to set</param>
    public void setPriority(int priority)
    {
        agent.avoidancePriority = priority;
    }
    
    /// <summary>
    /// Sets a new destination for the visitor
    /// </summary>
    /// <param name="destination">The new destination to set</param>
    /// <param name="stoppingDistance">The stopping distance to set</param>
    public void setNewDestination(Vector3 destination, float stoppingDistance)
    {
        agent.SetDestination(destination);
        agent.stoppingDistance = stoppingDistance;
    }
    
    /// <summary>
    /// Returns the visitor's current position
    /// </summary>
    /// <returns>The visitor's current position</returns>
    public Vector3 getPosition()
    {
        return transform.position;
    }
    
    /// <summary>
    /// Called when the visitor enters a POI. Disables the visitor's collider and renderer
    /// </summary>
    public void goInPOI()
    {
        visitorRenderer.enabled = false;
        visitorCollider.enabled = false;
        agent.enabled = false;
    }
    
    /// <summary>
    /// Called when the visitor exits a POI. Warps the visitor to the out position and re-enables the collider and renderer.
    /// Sets a new destination for the visitor if there are remaining POIs to visit.
    /// </summary>
    /// <param name="outPosition">The position to warp the visitor to</param>
    public void exitPOI(Vector3 outPosition) {
        agent.enabled = true;
        agent.Warp(outPosition);
        isInPOI = false;
        visitorRenderer.enabled = true;
        visitorCollider.enabled = true;
        
        // If there are remaining POIs to visit, set a new destination
        if (nbRemainingPOIs > 0)
        {
            targetPOI = poisManager.GetRandomPOI(targetPOI).GetComponent<POI>();
            agent.SetDestination(targetPOI.GetInPoint());
            nbRemainingPOIs--;
        }
        // If there are no remaining POIs, set the spawn position as the destination to exit
        else
        {
            targetPOI = null;
            agent.SetDestination(spawnPosition);
            isExiting = true;
        }

        agent.stoppingDistance = 0.0f;
        agent.avoidancePriority = 0;
    }
}
