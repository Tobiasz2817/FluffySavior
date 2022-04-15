using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class FallingObject : BasicObject
{
    private Poller myPoller;
    
    void Start()
    {
        myPoller = gameObject.GetComponentInParent<Poller>();
        
        if (randomSpeed)
        {
            speedObject = Random.Range(0.5f, 6f);
        }

        direction = Vector2.down;
        
    }
    void Update()
    {
        if(isMove == false)
            transform.Translate(direction * Time.deltaTime * speedObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("I hit player");
        }
        else if (col.CompareTag("Bullet"))
        {
            myPoller.PoolObject(gameObject);
        }
        else if (col.CompareTag("Ground"))
        {
            myPoller.PoolObject(gameObject);
            
            Debug.Log("Booom o podłoge !");
        }
    }

    public override void NewRandomPosition()
    {
        transform.position = GetRandomPosition();
    }
}
