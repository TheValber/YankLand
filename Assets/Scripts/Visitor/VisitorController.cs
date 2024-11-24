using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class VisitorController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Renderer visitorRenderer = null;
    private Collider visitorCollider = null;
    
    private POISManager poisManager = null;
    private POI targetPOI = null;
    private bool isInPOI = false;
    
    public Vector3 spawnPosition = Vector3.zero;
    public int nbRemainingPOIs = 0;
    private bool isExiting = false;
    
    private UIManager uiManager = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        visitorRenderer = GetComponent<Renderer>();
        visitorCollider = GetComponent<Collider>();
        
        poisManager = GameObject.Find("POIs").GetComponent<POISManager>();
        
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        
        spawnPosition = transform.position;
        
        nbRemainingPOIs = Random.Range(1, 5);

        while (targetPOI == null)
        {
            targetPOI = poisManager.GetRandomPOI(null).GetComponent<POI>();
        }
        agent.SetDestination(targetPOI.GetInPoint());
        nbRemainingPOIs--;
    }

    void Update()
    {
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
            // Check if we've reached the destination
            if (agent.remainingDistance < 10.0f)
            {
                targetPOI.goInQueue(this);
                isInPOI = true;
                agent.radius = 0.6f;
                agent.avoidancePriority = 75;
            }
        }
    }
    
    public void setPriority(int priority)
    {
        agent.avoidancePriority = priority;
    }
    
    public void setNewDestination(Vector3 destination, float stoppingDistance)
    {
        agent.SetDestination(destination);
        agent.stoppingDistance = stoppingDistance;
    }
    
    public Vector3 getPosition()
    {
        return transform.position;
    }
    
    public void goInPOI()
    {
        visitorRenderer.enabled = false;
        visitorCollider.enabled = false;
        agent.enabled = false;
    }
    
    public void exitPOI(Vector3 outPosition) {
        agent.enabled = true;
        agent.Warp(outPosition);
        isInPOI = false;
        visitorRenderer.enabled = true;
        visitorCollider.enabled = true;
        
        if (nbRemainingPOIs > 0)
        {
            targetPOI = poisManager.GetRandomPOI(targetPOI).GetComponent<POI>();
            agent.SetDestination(targetPOI.GetInPoint());
            nbRemainingPOIs--;
        } else
        {
            targetPOI = null;
            agent.SetDestination(spawnPosition);
            isExiting = true;
        }

        agent.stoppingDistance = 0.0f;
        // agent.radius = 1.0f;
        agent.avoidancePriority = 0;
    }
}
