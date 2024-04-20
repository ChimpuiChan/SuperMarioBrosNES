using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameRate;

    private SpriteRenderer spriteRenderer;
    private int currentFrame; // Current frame in sprite array

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // OnEnable and OnDisable are those code blocks that execute when this SpriteAnimator script is enabled or disabled
    private void OnEnable()
    {
        // nameof(Animation) is to convert to string so if we change the function name we get warnings
        // Just a safer bet, well personally I like the direct string better
        InvokeRepeating(nameof(Animate), frameRate, frameRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        currentFrame++;

        if (currentFrame >= sprites.Length)
        {
            currentFrame = 0;
        }

        // Just to make sure it doesn't go out of bounds
        // Not really necessary though but why not ;)
        if (currentFrame >= 0 && currentFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[currentFrame];
        }
        
    }
}
