using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float turnSpeed = 10f; // Speed at which the enemy turns to face the target

    [SerializeField] private Transform[] waypoints;

    private int waypointIndex = 0;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false; // Disable automatic rotation to control it manually if needed
        navMeshAgent.avoidancePriority = Mathf.RoundToInt(navMeshAgent.speed * 10f); // Set avoidance priority based on speed
    }

    private void Start()
    {
        waypoints = FindFirstObjectByType<WaypointManager>().GetWaypoints();
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
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return Vector3.zero;
        }

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0; // Reset to the first waypoint if all have been visited
            // return transform.position; // Return current position if no more waypoints are available
        }

        Vector3 nextWaypoint = waypoints[waypointIndex].position;
        waypointIndex++;

        return nextWaypoint;
    }
}
