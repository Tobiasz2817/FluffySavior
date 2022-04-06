using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FallingObject : BasicObject
{
    [SerializeField]
    protected float speedItem;

    [SerializeField]
    protected Vector2 direction;

    protected Vector2 startPos;

    void Start()
    {
        startPos = GetStartPosition();
        
        
        transform.position = startPos;
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speedItem);
    }

    private Vector2 GetStartPosition()
    {
        int x = Random.Range(-10, 10);
        int y = Random.Range(6, 10);

        Vector2 startPos = new Vector2(x, y);

        return startPos;
    }
}
