using UnityEngine;

public class MarioAnimations : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MarioMovement marioMovement;

    public Sprite idle;
    public Sprite jump;
    public SpriteAnimator run;
    public Sprite slide;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        marioMovement = GetComponentInParent<MarioMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        // Only enabled when isRunning = true
        // If enabled, the AnimatorSprite script is enabled which means it executes the block under the OnEnabled() function
        // Which is apparenly InvokeRepeating(Animate, frameRate, frameRate)
        run.enabled = marioMovement.isRunning;

        // Order of defining if statements is crucial for sliding to work
        if (marioMovement.isJumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (marioMovement.isSliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!marioMovement.isRunning)
        {
            // If not running, not sliding, not jumping, then Mario is obviously idle
            spriteRenderer.sprite = idle;
        }
        else
        {
            // Just defining this coz OCD
        }
    }

}
