using UnityEngine;

public class GoombaSquish : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite squishedGoomba;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            // the transform inside the DotProd() is Goomba's transform
            // The collision direction and Goomba direction are compared using DotProd to detect if Mario landed on Goomba's head
            if (collision.transform.DotProd(transform, Vector2.down))
            {
                Squish();
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



