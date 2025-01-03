using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float fallThreshold = -10f; // Y-position threshold for respawn
    public Vector3 respawnPosition;    // Respawn position

    private Rigidbody2D rb;
    private bool canControl = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position; // Set the initial spawn position
    }

    void Update()
    {
        if (!canControl) return; // Block all movement if control is disabled

        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Check if the player has fallen below the threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    public void SetControl(bool value)
    {
        canControl = value;
    }

    public void Move(float direction)
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void JumpAndMove(float horizontalDirection)
    {
        rb.velocity = new Vector2(horizontalDirection * moveSpeed, jumpForce);
    }

    public void Respawn()
    {
        Debug.Log("Respawning player...");

        // Reset player position and velocity
        transform.position = respawnPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Debug.Log("Player respawned at: " + respawnPosition);
    }

    public void SetRespawnPosition(Vector3 newPosition)
    {
        respawnPosition = newPosition;
        Debug.Log("Respawn position updated to " + newPosition);
    }
}
