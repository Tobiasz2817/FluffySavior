using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class BasicObject : MonoBehaviour
{
    [SerializeField] 
    protected bool randomSpeed;
    [SerializeField]
    [Range(0.5f,6f)]
    protected float speedObject;
    
    protected Vector2 direction;
    protected Vector2 startPos;
    protected Vector2 GetRandomPosition()
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth,camera.pixelHeight));
        
        // Y 
        float yPosCamera = camera.gameObject.transform.position.y;
        float upperCornerCamera = pos.y;
        
        float conditionDistanceY = Random.Range(4f,8f);
        float startFrom = (upperCornerCamera + conditionDistanceY) + yPosCamera;
        float y = Random.Range(startFrom, Random.Range(startFrom, startFrom + 6f));
        
        // X
        float conditionDistanceX = Random.Range(2f,4f);
        float x = Random.Range((pos.x * -1) + conditionDistanceX, pos.x - conditionDistanceX);
        

        return new Vector2(x, y);
    }
}
