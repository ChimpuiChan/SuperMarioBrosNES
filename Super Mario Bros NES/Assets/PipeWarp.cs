using System.Collections;
using UnityEngine;

public class PipeWarp : MonoBehaviour
{
    public Transform warpExit;
    public KeyCode entranceKey = KeyCode.S;

    public Vector3 enterDirection = Vector3.down;

    // Usually Mario just exits the pipe and teleports to some random place instead of coming of a pipe and we use Vector3.zero to specify that by default
    public Vector3 exitDirection = Vector3.zero;

    // Warping animation parameters
    private Vector3 enteredPosition;
    private Vector3 enteredSize;
    private float elapsedTime;
    private float animationDuration;
    private float lerpTime;
    private Vector3 startPosition;
    private Vector3 startSize;
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (warpExit != null && other.CompareTag("Player"))
        {
            if (Input.GetKey(entranceKey))
            {
                // If there is a exit from this pipe and collision happened with the player provided the player pressed the "down" button in their controller, start the warp animation
                StartCoroutine(WarpPipeAnimation(other.transform));
            }
        }
    }

    private IEnumerator WarpPipeAnimation(Transform player)
    {
        player.GetComponent<MarioMovement>().enabled = false;

        enteredPosition = transform.position + enterDirection;
        enteredSize = Vector3.one * 0.5f; // Reduce mario size when entering pipe

        yield return WarpPipe(player.transform, enteredPosition, enteredSize);
        yield return new WaitForSeconds(1f);

        // Exit Animation
        if (exitDirection != Vector3.zero)
        {
            // Now we have to animate in the opposite direction
            player.position = warpExit.position - exitDirection;
            yield return WarpPipe(player, warpExit.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = warpExit.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<MarioMovement>().enabled = true;
    }

    private IEnumerator WarpPipe(Transform player, Vector3 endPosition, Vector3 endSize)
    {
        elapsedTime = 0f;
        animationDuration = 1f;

        startPosition = player.position;
        startSize = player.localScale;

        while (elapsedTime < animationDuration)
        {
            lerpTime = elapsedTime / animationDuration;

            player.position = Vector3.Lerp(startPosition, endPosition, lerpTime);
            player.localScale = Vector3.Lerp(startSize, endSize, lerpTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endSize;
    }
}
