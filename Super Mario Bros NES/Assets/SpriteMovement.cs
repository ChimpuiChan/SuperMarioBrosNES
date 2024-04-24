using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 2;

    // Generally most enemies move left by default in Mario games
    public Vector2 direction = Vector2.left;
    public Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Disable this script initially
        enabled = false;
    }

    // OnBecameVisible and OnBecameInvisible are pre-defined functions in unity that checks if this game object is on the screen or entered the camera view
    // Here we are using it to enable or disable this script
    // Enable when sprite on screen and disable when not on screen
    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    // Enable rigidbody when enabled = true (this script)
    private void OnEnable()
    {
        rb.WakeUp();
    }

    // If enabled = false, put rb to sleep
    // This will hold the values of the game object such as position, velocity etc., and resumes when script is enabled again
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * moveSpeed;

        // The Physics2D.gravity.y accesses the gravity defined in the project settings under the Physics2D settings
        // We can also use a separate gravity variable and assign to it
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        // Multiplying with Time.fixedDeltaTime coz gravity is -9.8m/s^2
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (rb.Raycast(Vector2.down))
        {
            // When falling down, velocity is negative anyway and when the raycast touches the ground (Default layer here) it gets set back to 0 since velocity.y is negative and 0 is greater
            // This ensures that the velocity does not keep increasing indefinitely
            velocity.y = Mathf.Max(velocity.y, 0);
        }

        if (rb.Raycast(direction))
        {
            // When colliding with any object in default layer, flip the direction to opposite
            direction = -direction;
        }
    }
}
