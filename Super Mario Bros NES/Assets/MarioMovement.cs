using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 8f;
    private Vector2 velocity;
    private float inputAxis;
    private Vector2 position;
    private Camera cam;
    private Vector2 leftEdge;
    private Vector2 rightEdge;
    
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
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        // Getting screen bounds.
        leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // For Mario to stay within the screen bounds.
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x -0.5f);
        rb.MovePosition(position);
    }
}
