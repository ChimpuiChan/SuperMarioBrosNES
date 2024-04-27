using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    // Making it public coz some cases we might have multiple sprite renderers so we manually assign them
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    // AnimateDeath coroutine parameters
    public float elapsedTime = 0f;
    public float duration = 3f;
    public float jumpForce = 10f;
    public float gravity = -36; // This value is based on Mario movement script parameters
    public Vector3 velocity;

    public SpriteAnimator spriteAnimator;

    // Called when the script is first attached to the game object
    // Will also be called when you press the reset button in the inspector menu
    // Best used to set the values to default
    // This happens in the editor
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        DisablePhysics();
        UpdateSprite();
        StartCoroutine(AnimateDeath());
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;

        // A higher value so that the sprite remains on top of all the objects
        spriteRenderer.sortingOrder = 10;

        // Since some sprites don't have separate dead sprite asset, so we perform a null check
        if (deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite;
        }
    }

    private void DisablePhysics()
    {
        // Some sprites have multiple colliders attached to them in some scenarios
        // We store them all in an array and traverse through it disabling each one
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        // RigidBody does not have an opeion to disable it, so we set isKinematic = true
        // This makes the game object not get affected by the physics engine
        GetComponent<Rigidbody2D>().isKinematic = true;

        // We are also disabling the movement script for both player and enemy
        // We get both components and perform a null check to disable them
        MarioMovement marioMovement = GetComponent<MarioMovement>();
        SpriteMovement spriteMovement = GetComponent<SpriteMovement>();


        if (marioMovement != null)
        {
            marioMovement.enabled = false;
            spriteAnimator.enabled = false;
        }

        if (spriteMovement != null)
        {
            spriteMovement.enabled = false;
            spriteAnimator.enabled = false;
        }
    }

    // For animating without using update function, we can use coroutines
    // We need to import System.Collections for this
    // Change the return type for this function to IEnumerator
    // Coroutines are used to delay the function execution and split their execution in multiple frames
    private IEnumerator AnimateDeath()
    {
        velocity = Vector3.up * jumpForce;

        while (elapsedTime < duration)
        {
            // Multiplying with Time.deltaTime twice coz gravity is m/s^2
            transform.position += velocity * Time.deltaTime;

            // Gradually decrease velocity.y using gravity so sprite falls down
            velocity.y += gravity * Time.deltaTime;
            elapsedTime += Time.deltaTime;

            // This waits until next frame for execution
            yield return null;
        }
    }
}
