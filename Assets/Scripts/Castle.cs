using UnityEngine;

public class Castle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Handle enemy entering the castle area
            Debug.Log("Enemy has entered the castle area.");
            
            Destroy(other.gameObject); // Destroy the enemy or handle it as needed
        }
    }
}
