using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform waypoint;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Start()
    {
        if (waypoint != null)
        {
            navMeshAgent.SetDestination(waypoint.position);
        }
        else
        {
            Debug.LogWarning("Waypoint is not assigned for the enemy.");
        }
    }
}
