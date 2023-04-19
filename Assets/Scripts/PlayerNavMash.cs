using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMash : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private Transform wayStartLevel;

    private NavMeshAgent navMashAgent;

    private void Awake()
    {
        navMashAgent = GetComponent<NavMeshAgent>();
    }
   

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { StartState(); }
        navMashAgent.destination = movePositionTransform.position;
    }
    void StartState() {
        navMashAgent.speed = 0f;
        transform.position = wayStartLevel.position;
        navMashAgent.speed = 3f;
        navMashAgent.destination = movePositionTransform.position;
        
    }
}
