using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 61;
    }
    
}
