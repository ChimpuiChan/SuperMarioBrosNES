using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class MarioState : MonoBehaviour
{
    // Getting what state Mario is currently in
    // This script handles Mario's animations and is attached to both small and big Mario
    public MarioAnimation smallMario;
    public MarioAnimation superMario;
    private MarioAnimation activeMario;

    private CapsuleCollider2D capsuleCollider;

    private DeathAnimation deathAnimation;

    // If small or big Mario script is active, then that is the current state of Mario and is set accordingly to the boolean variable
    public bool super => superMario.enabled;
    public bool small => smallMario.enabled;
    public bool dead => deathAnimation.enabled;

    // Grow or Shrink animation parameters
    private float elapsedTime;
    private float animationDuration;

    // StarMan animation parameters
    private float timeElapsed;
    public bool starPower { get; private set; }

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeMario = smallMario;
    }
    public void GotHit()
    {
        if (!dead && !starPower)
        {
            if (super)
            {
                ShrinkMario();
            }
            else
            {
                DeathOfMario();
            }
        }

        
    }

    private void ShrinkMario()
    {
        smallMario.enabled = true;
        superMario.enabled = false;
        activeMario = smallMario;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    public void GrowMario()
    {
        smallMario.enabled = false;
        superMario.enabled = true;
        activeMario = superMario;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    private void DeathOfMario()
    {
        // Disable animations and enable death animation
        smallMario.enabled = false;
        superMario.enabled = false;
        deathAnimation.enabled = true;

        // Reset level after 3 seconds for death animation to play
        GameManager.instance.MarioDeath(3f);
    }

    private IEnumerator ScaleAnimation()
    {
        elapsedTime = 0f;
        animationDuration = 0.5f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                // Display this animation for every 4 frames for 0.5 seconds
                // Small Mario will be disabled, then Super Mario will be enabled and next frame, small Mario will be enabled and Super Mario will be disabled
                // This way, it just flickers between the two for 0.5 seconds every 4 frames
                smallMario.enabled = !smallMario.enabled;
                superMario.enabled = !smallMario.enabled;
            }

            yield return null;
        }

        // We don't know what is the state of Mario currently, so we just disable both small and super and enable the active Mario
        smallMario.enabled = false;
        superMario.enabled = false;
        activeMario.enabled = true;
    }

    public void StarPower(float starManDuration)
    {
        StartCoroutine(StarManAnimation(starManDuration));
    }

    private IEnumerator StarManAnimation(float duration)
    {
        starPower = true;

        timeElapsed = 0f;
        
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                // Randomizing Hue while keeping Saturation and Value same
                activeMario.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        // Back to original color
        activeMario.spriteRenderer.color = Color.white;

        starPower = false;
    }
}
