using UnityEngine;

public class MarioState : MonoBehaviour
{
    // Getting what state Mario is currently in
    // This script handles Mario's animations and is attached to both small and big Mario
    public MarioAnimation smallMario;
    public MarioAnimation superMario;

    private DeathAnimation deathAnimation;

    // If small or big Mario script is active, then that is the current state of Mario and is set accordingly to the boolean variable
    public bool super => superMario.enabled;
    public bool small => smallMario.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }
    public void GotHit()
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

    private void ShrinkMario()
    {
        // Coming soon
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
}
