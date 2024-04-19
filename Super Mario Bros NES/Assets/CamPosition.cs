using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    private Transform marioPos;
    private Vector3 camPos;

    private void Awake()
    {
        marioPos = GameObject.FindWithTag("Player").transform;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
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
}
