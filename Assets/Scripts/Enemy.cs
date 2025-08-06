using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float turnSpeed = 10f; // Speed at which the enemy turns to face the target

    [SerializeField] private Transform[] waypoint;

    private int waypointIndex = 0;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false; // Disable automatic rotation to control it manually if needed
        navMeshAgent.avoidancePriority = Mathf.RoundToInt(navMeshAgent.speed * 10f); // Set avoidance priority based on speed
    }

    private void Start()
    {
        if (waypoint != null)
        {
            Vector3 nextWaypoint = GetNextWaypoint();
            if (nextWaypoint != Vector3.zero)
            {
                navMeshAgent.SetDestination(nextWaypoint);
            }
            else
            {
                Debug.LogWarning("Next waypoint is not valid.");
            }
        }
        else
        {
            Debug.LogWarning("Waypoint is not assigned for the enemy.");
        }
    }

    private void Update()
    {
        if (navMeshAgent.hasPath)
        {
            FaceTarget(navMeshAgent.steeringTarget);
        }
        else if (navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.LogWarning("NavMesh path is invalid.");
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Vector3 nextWaypoint = GetNextWaypoint();
            if (nextWaypoint != Vector3.zero)
            {
                navMeshAgent.SetDestination(nextWaypoint);
            }
        }
        else if (navMeshAgent.pathPending)
        {
            Debug.Log("Waiting for path to be calculated.");
        }
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // Keep the direction horizontal

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }
    
    private Vector3 GetNextWaypoint()
    {
        if (waypoint.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return Vector3.zero;
        }

        if (waypointIndex >= waypoint.Length)
        {
            return transform.position; // Return current position if no more waypoints are available
        }

        Vector3 nextWaypoint = waypoint[waypointIndex].position;
        waypointIndex++;

        return nextWaypoint;
    }
}
