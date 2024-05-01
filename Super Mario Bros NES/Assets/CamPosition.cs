using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    private Transform marioPos;
    private Vector3 camPos;

    public float defaultHeight = 0f;
    public float underGroundHeight = -25f;

    private void Awake()
    {
        marioPos = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        camPos = transform.position;

        // Compare if Mario's position is greater or camera's position is greater.
        // Whatever is greater, that value is taken.
        // This is for locking camera to not to left.
        camPos.x = Mathf.Max(camPos.x, marioPos.position.x);
        transform.position = camPos;
    }

    public void UnderGroundCamera(bool inUnderGround)
    {
        camPos = transform.position;
        camPos.y = inUnderGround ? underGroundHeight : defaultHeight;
        transform.position = camPos;
    }
}
