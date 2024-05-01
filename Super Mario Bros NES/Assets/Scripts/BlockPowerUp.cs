using System.Collections;
using UnityEngine;

public class BlockPowerUp : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D physicsCollider;
    private BoxCollider2D triggerCollider;
    private SpriteRenderer spriteRenderer;
    private float elapsedTime;
    private float animationDuration;
    private float lerpTime;
    private Vector3 startPosition;
    private Vector3 endPosition;


    private void Start()
    {
        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<CircleCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Disable all the physics while spawning the power up item
        // Also disabling the sprite renderer for 0.25s for the block hit animation to play or we gonna see the hidden power up inside while the block is being animated
        rb.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        elapsedTime = 0f;
        animationDuration = 0.5f;


        startPosition = transform.localPosition;

        // 1 unit above block is end position
        endPosition = transform.localPosition + Vector3.up;


        while (elapsedTime < animationDuration)
        {
            lerpTime = elapsedTime / animationDuration;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, lerpTime);
            elapsedTime += Time.deltaTime;

            // Wait for next frame
            yield return null;
        }

        // Again, just making sure it reaches the end position
        transform.localPosition = endPosition;

        // Now enable all the components after the animation is finished
        rb.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }

}
