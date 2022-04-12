using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class FallingObject : BasicObject
{
    void Start()
    {
        if (randomSpeed)
        {
            speedObject = Random.Range(0.5f, 6f);
        }
        startPos = GetRandomPosition();
        
        direction = Vector2.down;

        transform.position = startPos;
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speedObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("I hit player");
        }
    }
}
