using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private Vector3 blockPosition;
    private Vector3 upwardPosition;
    private float elapsedTime;
    private float animationDuration;
    private float lerpTime;

    private void Start()
    {
        GameManager.instance.AddCoin();
        StartCoroutine(HitAnimation());
    }

    // Copy pasted the animation code from the BlockHit script
    private IEnumerator HitAnimation()
    {
        blockPosition = transform.localPosition;
        upwardPosition = blockPosition + Vector3.up * 2f;

        yield return MoveBlock(blockPosition, upwardPosition);
        yield return MoveBlock(upwardPosition, blockPosition);

        Destroy(gameObject);
    }

    private IEnumerator MoveBlock(Vector3 currentPosition, Vector3 targetPosition)
    {
        elapsedTime = 0f;
        animationDuration = 0.25f;

        while (elapsedTime < animationDuration)
        {
            lerpTime = elapsedTime / animationDuration;

            transform.localPosition = Vector3.Lerp(currentPosition, targetPosition, lerpTime);
            elapsedTime += Time.deltaTime * 1.5f;

            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}
