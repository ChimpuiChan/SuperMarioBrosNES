using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    // -1 means the block can be hit infinite number of times
    // It just comes down to the fact that we don't actually get to 0, so since we haven't specified anything to do when it is less than 0, it just simply does nothing
    public int maxHits = -1;
    private SpriteRenderer spriteRenderer;
    public Sprite hitState;

    // Need this boolean variable for making sure that Mario cannot hit the block while it's animating
    private bool isAnimating;

    // For animating block when hit
    private Vector3 blockPosition;
    private Vector3 upwardPosition;
    private float elapsedTime;
    private float animationDuration;
    private float lerpTime;

    // Block contents
    public GameObject item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAnimating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotProd(transform, Vector2.up))
            {
                // If game object collided with is Mario and if he is colliding with his head, then he has hit the block provided that the block is not currently animating and it has maxHits atleast 1
                // For maxHits = -1, the block can be hit infinite number of times
                // collision.transform is Mario and transform is the transform of this block
                // We are comparing the direction of Mario's hit to the block
                HitBlock();
            }
        }
    }

    private void HitBlock()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // For hidden blocks to appear when hit
        spriteRenderer.enabled = true;

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = hitState;
        }

        if (item != null)
        {
            // Spawn the item when block is hit at the same positionof this block
            // The rest of the things will be taken care of the scripts attached to the item itself
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(HitAnimation());
    }

    // For animating the block when hit using Math :P
    private IEnumerator HitAnimation()
    {
        isAnimating = true;

        // transform.localPosition is used to move this object relative to its parent
        // Suppose in the world space, the parent is at coordinates (0, 0, 0) and this block that is attached to it is 1 unit above from world space, the world space coordinates of the block will be (1, 0, 0)
        // However it's local space coordinates are relative to its parent's coordinates and hence its local position is at coordinates (0, 0, 0)
        // This is done to ensure that if this object is attached to any parent object, it still moves relative to its parent while still being able to move independantly
        blockPosition = transform.localPosition;
        upwardPosition = blockPosition + Vector3.up * 0.5f;

        // Move up
        yield return MoveBlock(blockPosition, upwardPosition);

        // Come down
        yield return MoveBlock(upwardPosition, blockPosition);

        isAnimating = false;
    }

    private IEnumerator MoveBlock(Vector3 currentPosition, Vector3 targetPosition)
    {
        elapsedTime = 0f;
        animationDuration = 0.125f;

        while (elapsedTime < animationDuration)
        {
            lerpTime = elapsedTime / animationDuration;

            transform.localPosition = Vector3.Lerp(currentPosition, targetPosition, lerpTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Just to make sure it has reached the target position coz elapsed time may not always reach the exact value of animation duration
        transform.localPosition = targetPosition;
    }
}
