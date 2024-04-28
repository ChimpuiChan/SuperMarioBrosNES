using Unity.VisualScripting;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.MarioDeath();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
