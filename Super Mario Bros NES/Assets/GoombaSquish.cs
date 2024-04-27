using UnityEngine;

public class GoombaSquish : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite squishedGoomba;
    private MarioState marioState;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get MarioState script when collided with the player
            marioState = collision.gameObject.GetComponent<MarioState>();

            // The transform inside the DotProd() is Goomba's transform
            // The collision direction and Goomba direction are compared using DotProd to detect if Mario landed on Goomba's head
            if (collision.transform.DotProd(transform, Vector2.down))
            {
                Squish();
            }
            else
            {
                // If Mario did not land on goomba's head, then he has to take hit
                marioState.GotHit();
            }
        }
    }

    private void Squish()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteAnimator>().enabled = false;
        GetComponent<SpriteMovement>().enabled = false;
        spriteRenderer.sprite = squishedGoomba;
        Destroy(gameObject, 0.5f); // Destroy after 0.5 seconds
    }

}



