using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class VisitorController : MonoBehaviour
{
    private float speed = 10.0f;
    
    private NavMeshAgent agent;
    private Renderer visitorRenderer;
    private Collider visitorCollider;
    
    private POISManager poisManager;
    private POI targetPOI;
    private bool isInPOI = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        
        visitorRenderer = GetComponent<Renderer>();
        visitorCollider = GetComponent<Collider>();
        
        poisManager = GameObject.Find("POIs").GetComponent<POISManager>();

        while (targetPOI == null)
        {
            targetPOI = poisManager.GetRandomPOI(null).GetComponent<POI>();
        }
        agent.SetDestination(targetPOI.GetInPoint());
    }

    void Update()
    {
        if (!isInPOI && agent.hasPath && !agent.pathPending && (agent.remainingDistance < 10.0f || (targetPOI.getLastQueuePosition() - transform.position).magnitude < 10.0f))
        {
            targetPOI.goInQueue(this);
            isInPOI = true;
        }
    }
    
    public void setNewDestination(Vector3 destination, float stoppingDistance)
    {
        agent.SetDestination(destination);
        agent.stoppingDistance = stoppingDistance;
        agent.radius = 0.6f;
    }
    
    public Vector3 getPosition()
    {
        return transform.position;
    }
    
    public void goInPOI()
    {
        visitorRenderer.enabled = false;
        visitorCollider.enabled = false;
    }
    
    public void exitPOI(Vector3 outPosition) {
        agent.Warp(outPosition);
        isInPOI = false;
        visitorRenderer.enabled = true;
        visitorCollider.enabled = true;
        
        targetPOI = poisManager.GetRandomPOI(targetPOI).GetComponent<POI>();
        agent.SetDestination(targetPOI.GetInPoint());
        agent.stoppingDistance = 0.0f;
        agent.radius = 1.5f;
    }
}
