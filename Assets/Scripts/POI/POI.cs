using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    private Transform inPoint;
    private Transform outPoint;
    
    [SerializeField] int maxPersonsInPOI = 3;
    [SerializeField] float timeInPOI = 3.0f;
    
    private Queue<VisitorController> queue = new Queue<VisitorController>();
    private Queue<VisitorController> inThePOI = new Queue<VisitorController>();
    private Queue<float> inPOIQueueTime = new Queue<float>();
    private bool hasToUpdateQueue = false;
    
    private Vector3 lastQueuePosition;
    
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
                    visitor.setNewDestination(previousPosition, 0.0f);

                }
                if ((visitor.getPosition() - previousPosition).magnitude > 2.5f)
                {
                    visitor.setNewDestination(previousPosition, 2.0f);
                    hasToUpdateQueue = true;
                }

                index++;
                previousPosition = visitor.getPosition();
            }
            lastQueuePosition = previousPosition;
        }
    }
    
    private void enterPOI()
    {
        if (queue.Count > 0 && inThePOI.Count < maxPersonsInPOI && (queue.Peek().getPosition() - inPoint.position).magnitude < 1.5f)
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
    
    public Vector3 getLastQueuePosition()
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
