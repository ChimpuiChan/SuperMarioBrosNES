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
                GameManager.instance.AddCoin();
                break;

            case PowerUpType.SuperMushroom:
                player.GetComponent<MarioState>().GrowMario();
                break;

            case PowerUpType.GreenMushroom:
                GameManager.instance.AddLife();
                break;

            case PowerUpType.StarMan:
                player.GetComponent<MarioState>().StarPower(10f);
                break;
        }

        Destroy(gameObject);
    }
}
