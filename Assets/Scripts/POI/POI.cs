using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Point Of Interest (POI) where visitors can queue and enter.
/// Manages the queue, entry, anb exit of visitors in the POI.
/// </summary>
public class POI : MonoBehaviour
{
    // --- In and out points references ---
    private Transform inPoint = null; // Entry point of the POI
    private Transform outPoint = null; // Exit point of the POI
    
    // --- Configurantion ---
    [SerializeField] int maxPersonsInPOI = 3; // Maximum number of persons in the POI
    [SerializeField] float timeInPOI = 3.0f; // Time a visitor stays in the POI
    
    // --- State ---
    private Queue<VisitorController> queue = new Queue<VisitorController>(); // Queue of visitors waiting to enter the POI
    private Queue<VisitorController> inThePOI = new Queue<VisitorController>(); // Visitors currently in the POI
    private Queue<float> inPOIQueueTime = new Queue<float>(); // Time at which visitors entered the POI
    private bool hasToUpdateQueue = false; // Flag to indicate if the queue needs to be updated
    private Vector3 lastQueuePosition = Vector3.zero; // Last known position of the queue

    public int inPOICount; // For debugging purposes: current count of visitors in the POI
    
    /// <summary>
    /// Initializes the POI by finding the entry and exit points.
    /// </summary>
    void Start()
    {
        inPoint = transform.Find("InPoint");
        outPoint = transform.Find("OutPoint");
    }

    /// <summary>
    /// Updates the state of the POI each frame.
    /// </summary>
    void Update()
    {
        enterPOI();
        exitPOI();
        updateQueue();
        inPOICount = inThePOI.Count;
    }
    
    /// <summary>
    /// Returns the position of the entry point of the POI.
    /// </summary>
    /// <returns>The position of the entry point of the POI.</returns>
    public Vector3 GetInPoint()
    {
        return inPoint.position;
    }
    
    /// <summary>
    /// Adds a visitor to the queue of the POI.
    /// </summary>
    /// <param name="visitor">The visitor to add to the queue.</param>
    public void goInQueue(VisitorController visitor)
    {
        queue.Enqueue(visitor);
        hasToUpdateQueue = true; // The queue needs to be updated
    }
    
    /// <summary>
    /// Updates the position of the visitors in the queue.
    /// </summary>
    private void updateQueue()
    {
        if (hasToUpdateQueue)
        {
            hasToUpdateQueue = false;
            int index = 0;
            Vector3 previousPosition = inPoint.position;
            
            // Iterate through the visitors in the queue to update their destination and priority
            foreach (VisitorController visitor in queue)
            {
                if (index == 0)
                {
                    visitor.setNewDestination(previousPosition, 0.1f);
                }
                
                // If a visitor is too far from the previous one, update its destination
                if ((visitor.getPosition() - previousPosition).magnitude > 1.5f)
                {
                    visitor.setNewDestination(previousPosition, 1.2f);
                    hasToUpdateQueue = true;
                }
                
                visitor.setPriority(index);

                index++;
                previousPosition = visitor.getPosition();
            }
            
            lastQueuePosition = previousPosition;
        }
    }
    
    /// <summary>
    /// Moves visitors from the queue to the POI if there's space and they're close enough.
    /// </summary>
    private void enterPOI()
    {
        if (queue.Count > 0 && inThePOI.Count < maxPersonsInPOI && (queue.Peek().getPosition() - inPoint.position).magnitude < 1.0f)
        {
            VisitorController visitor = queue.Dequeue();
            visitor.goInPOI();
            inThePOI.Enqueue(visitor);
            inPOIQueueTime.Enqueue(Time.time);
            hasToUpdateQueue = true; // The queue needs to be updated
        }
    }
    
    /// <summary>
    /// Removes visitors from the POI after they have stayed for a certain amount of time.
    /// </summary>
    private void exitPOI()
    {
        if (inThePOI.Count > 0 && Time.time - inPOIQueueTime.Peek() > timeInPOI)
        {
            VisitorController visitor = inThePOI.Dequeue();
            inPOIQueueTime.Dequeue();
            visitor.exitPOI(outPoint.position); // Move the visitor to the exit point
        }
    }
    
    /// <summary>
    /// Visualizes the queue and the state of the POI in the Scene view.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 previousPosition = inPoint != null ? inPoint.position : Vector3.zero;
        foreach (VisitorController visitor in queue)
        {
            if (visitor != null)
            {
                Gizmos.DrawLine(previousPosition, visitor.getPosition());
                previousPosition = visitor.getPosition();
            }
        }

        if (hasToUpdateQueue)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}
