using System.Collections;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castleEntrance;
    public float speed = 6f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(AnimationMovement(flag, poleBottom.position));
            StartCoroutine(EndLevelAnimation(other.transform));
        }
    }


    private IEnumerator EndLevelAnimation(Transform player)
    {
        player.GetComponent<MarioMovement>().enabled = false;

        yield return AnimationMovement(player, poleBottom.position);
        yield return AnimationMovement(player, player.position + Vector3.right);
        yield return AnimationMovement(player, player.position + Vector3.right + Vector3.down);
        yield return AnimationMovement(player, castleEntrance.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.instance.NextLevel();
    }

    private IEnumerator AnimationMovement(Transform entity, Vector3 destination)
    {
        while (Vector3.Distance(entity.position, destination) > 0.125f)
        {
            entity.position = Vector3.MoveTowards(entity.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        entity.position = destination;
    }
}
