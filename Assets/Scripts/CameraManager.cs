using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset;   // Offset between the camera and the player
    public float smoothSpeed = 0.125f; // Smooth transition speed

    void Start()
    {
        // Automatically calculate the offset if not set manually in the Inspector
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Lock the camera's vertical position
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);

        // Smoothly interpolate the camera's position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
