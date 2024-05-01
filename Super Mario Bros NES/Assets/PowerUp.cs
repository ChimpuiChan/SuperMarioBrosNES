using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Coin,
        SuperMushroom,
        GreenMushroom,
        StarMan
    }

    public PowerUpType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectPowerUp(other.gameObject);
        }
    }

    private void CollectPowerUp(GameObject player)
    {
        // My first switch case statements :3
        // Tip: Type 'sw' and press enter, type enum name and press down arrow for automatically creating switch case statements for all types
        switch (type)
        {
            case PowerUpType.Coin:
                break;

            case PowerUpType.SuperMushroom:
                break;

            case PowerUpType.GreenMushroom:
                break;

            case PowerUpType.StarMan:
                break;
        }

        Destroy(gameObject);
    }
}
