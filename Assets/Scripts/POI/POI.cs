using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    private Transform inPoint = null;
    private Transform outPoint = null;
    
    [SerializeField] int maxPersonsInPOI = 3;
    [SerializeField] float timeInPOI = 3.0f;
    
    private Queue<VisitorController> queue = new Queue<VisitorController>();
    private Queue<VisitorController> inThePOI = new Queue<VisitorController>();
    private Queue<float> inPOIQueueTime = new Queue<float>();
    private bool hasToUpdateQueue = false;
    
    private Vector3 lastQueuePosition = Vector3.zero;

    public int inPOIQueueCount; // For debugging purposes
    
    void Start()
    {
        inPoint = transform.Find("InPoint");
        outPoint = transform.Find("OutPoint");
    }

    void Update()
    {
        enterPOI();
        exitPOI();
        updateQueue();
        inPOIQueueCount = inThePOI.Count;
    }
    
    public Vector3 GetInPoint()
    {
        return inPoint.position;
    }
    
    public void goInQueue(VisitorController visitor)
    {
        queue.Enqueue(visitor);
        hasToUpdateQueue = true;
    }
    
    private void updateQueue()
    {
        if (hasToUpdateQueue)
        {
            hasToUpdateQueue = false;
            int index = 0;
            Vector3 previousPosition = inPoint.position;
            foreach (VisitorController visitor in queue)
            {
                if (index == 0)
                {
                    visitor.setNewDestination(previousPosition, 0.1f);

                }
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
    
    private void enterPOI()
    {
        if (queue.Count > 0 && inThePOI.Count < maxPersonsInPOI && (queue.Peek().getPosition() - inPoint.position).magnitude < 1.0f)
        {
            VisitorController visitor = queue.Dequeue();
            visitor.goInPOI();
            inThePOI.Enqueue(visitor);
            inPOIQueueTime.Enqueue(Time.time);
            hasToUpdateQueue = true;
        }
    }
    
    private void exitPOI()
    {
        if (inThePOI.Count > 0 && Time.time - inPOIQueueTime.Peek() > timeInPOI)
        {
            VisitorController visitor = inThePOI.Dequeue();
            inPOIQueueTime.Dequeue();
            visitor.exitPOI(outPoint.position);
        }
    }
    
    public Vector3 getLastQueuePosition() // Est il pertinent pour les visiteurs de detecter la fin de la file d'attente ?
    {
        return lastQueuePosition;
    }
    
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
