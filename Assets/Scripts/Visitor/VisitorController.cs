using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisitorController : MonoBehaviour
{
    private float speed = 5.0f;
    
    private NavMeshAgent agent;

    [SerializeField] private GameObject[] targets;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1.0f)
        {
            agent.SetDestination(targets[Random.Range(0, targets.Length)].transform.position);
        }
    }
}
