using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperMoveCamera : MonoBehaviour
{
    [SerializeField]
    private float speedCamera;
    
    void Update()
    {
        transform.Translate(Vector3.up * speedCamera * Time.deltaTime);
    }

    public void SetSpeedCamera(float newSpeed)
    {
        speedCamera = newSpeed;
    }
}
