using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 

    private Rigidbody2D rb;
    private bool canControl = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canControl) return; // Block all movement if control is disabled

        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Flip player when moving left or right
        if (move > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (move < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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


}
