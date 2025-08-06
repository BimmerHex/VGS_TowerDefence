using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        float newRotationSpeed = rotationSpeed * 100f; // Convert to degrees per second
        transform.Rotate(rotationAxis * newRotationSpeed * Time.deltaTime);
    }
}
