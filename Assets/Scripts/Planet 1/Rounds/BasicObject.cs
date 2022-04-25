using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public abstract class BasicObject : MonoBehaviour
{
    [SerializeField] 
    protected bool randomSpeed;
    [SerializeField]
    [Range(0.5f,6f)]
    protected float speedObject;

    [SerializeField] 
    protected bool isMove = false;

    protected GameManager gameManager;
    public bool IsMove
    {
        set => isMove = value;
        get => isMove;
    }


    protected Vector2 direction;
    

    protected Vector2 GetRandomPosition()
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth,camera.pixelHeight));
        
        // Y 
        float upperCornerCamera = pos.y;
        
        float conditionDistanceY = Random.Range(5f,10f);
        float startFrom = upperCornerCamera + conditionDistanceY;
        float y = Random.Range(startFrom, Random.Range(startFrom, startFrom + 6f));
        
        // X
        float conditionDistanceX = Random.Range(2f,4f);
        float x = Random.Range((pos.x * -1) + conditionDistanceX, pos.x - conditionDistanceX);
        

        return new Vector2(x, y);
    }

    protected bool IsOnScene(Transform myPosition)
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth,camera.pixelHeight));
        
        if(myPosition.position.y < pos.y)
        {
            return true;
        }

        return false;
    }

    public abstract void NewRandomPosition();
}
