using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform pool;
    List<Transform> patrolPoints = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in pool)
        {
            patrolPoints.Add(child);
        }
    }
    void Update()
    {
        agent.destination = patrolPoints[0].position;
    }
}
