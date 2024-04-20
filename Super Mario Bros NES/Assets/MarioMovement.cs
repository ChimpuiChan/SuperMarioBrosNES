using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    // Movement
    private Rigidbody2D rb;
    public float moveSpeed = 8f;
    private Vector2 velocity;
    private float inputAxis;
    private Vector2 position;

    // Screen bounds
    private Camera cam;
    private Vector2 leftEdge;
    private Vector2 rightEdge;

    // Jumping
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    // The => means that we can perform calculations in the RHS and apply to LHS
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    // Making it have a public getter but a private setter so only this class can modify its value
    public bool isGrounded { get; private set; }
    public bool isJumping { get; private set; }
    private bool isFalling;
    private float gravMul;

    private void Start()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }


    private void Update()
    {
        HorizontalMovement();

        isGrounded = rb.Raycast(Vector2.down);

        if (isGrounded)
        {
            Jump();
        }

        ApplyGravity();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Max(velocity.y, 0);
        isJumping = velocity.y > 0;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            isJumping = true;
        }
    }

    private void ApplyGravity()
    {
        isFalling = velocity.y < 0 || !Input.GetButton("Jump");
        gravMul = isFalling ? 2f : 1f;
        velocity.y += gravity * gravMul * Time.deltaTime;

        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        // Getting screen bounds
        leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // For Mario to stay within the screen bounds
        // Clamp will make sure the position.x stays within the screen bounds value
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x -0.5f);
        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If Mario collides with any object other than "PowerUp" layer and direction of collision is "Vector2.up" which means whether Mario's head is hitting block, then velocity.y = 0 so Mario just falls down and doesn't get stuck to the block for a while
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotProd(collision.transform, Vector2.up))
            {
                velocity.y = 0;
            }
        }
    }
}
