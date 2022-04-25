using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLoopEnviroment : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        Vector3 pos = mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth,mainCamera.pixelHeight));
        if (transform.position.x > pos.x)
        {
            transform.position = new Vector2((pos.x * -1),transform.position.y);
        }
        else if (transform.position.x < (pos.x * -1))
        {
            transform.position = new Vector2(pos.x,transform.position.y);
        }
    }
}
