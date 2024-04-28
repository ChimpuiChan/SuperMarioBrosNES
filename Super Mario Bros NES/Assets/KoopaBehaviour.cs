using UnityEngine;

public class KoopaBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite koopaShell;
    private MarioState marioState;

    private bool inShell;
    private bool kicked;

    private Vector2 kickDirection;
    private SpriteMovement spriteMovement;
    public float shellSpeed = 12f; // Experimented close to original game value

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!inShell && collision.gameObject.CompareTag("Player"))
        {
            // Get MarioState script when collided with the player
            marioState = collision.gameObject.GetComponent<MarioState>();

            // The transform inside the DotProd() is Goomba's transform
            // The collision direction and Goomba direction are compared using DotProd to detect if Mario landed on Goomba's head
            if (collision.transform.DotProd(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                // If Mario did not land on goomba's head, then he has to take hit
                marioState.GotHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (inShell && other.CompareTag("Player"))
        {
            if (!kicked)
            {
                kickDirection = new Vector2(transform.position.x - other.transform.position.x, 0f);
                KickShell(kickDirection);
            }
            else
            {
                marioState = other.GetComponent<MarioState>();
                marioState.GotHit();
            }
        }
       else if (!inShell && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            // For Koopas that are not inside the shell to die when hit by a kicked Koopa shell
            KoopaDeath();
        }
    }

    private void EnterShell()
    {
        inShell = true;
        GetComponent<SpriteAnimator>().enabled = false;
        GetComponent<SpriteMovement>().enabled = false;
        spriteRenderer.sprite = koopaShell;

        // Changing the shell to "Shell" layer for killing other enemies by detecting collision coz "Enemy" to "Enemy" layer collision is disabled
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void KickShell(Vector2 kickDirection)
    {
        kicked = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        spriteMovement = GetComponent<SpriteMovement>();
        spriteMovement.direction = kickDirection.normalized; 
        spriteMovement.moveSpeed = shellSpeed;
        spriteMovement.enabled = true;

    }

    private void KoopaDeath()
    {
        GetComponent<SpriteAnimator>().enabled = false;
        GetComponent<SpriteMovement>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
    }

    private void OnBecameInvisible()
    {
        if (kicked)
        {
            // Destroy the shell after it is kicked and leaves the screen bounds
            Destroy(gameObject);
        }
    }

}



