using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SocialPlatforms;
using Object = System.Object;
using Random = UnityEngine.Random;

public class FallingObject : BasicObject
{
    private Poller myPoller;
    private Poller pointsPoller;

    void Start()
    {
        myPoller = gameObject.GetComponentInParent<Poller>();
        gameManager = GameManager.Instance;

        pointsPoller = GameObject.Find("PollerPoints").GetComponent<Poller>();
        
        if (randomSpeed)
        {
            speedObject = Random.Range(0.5f, 6f);
        }

        direction = Vector2.down;
        
    }
    void Update()
    {
        if(isMove == true)
            transform.Translate(direction * Time.deltaTime * speedObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameManager.isGameOver) return;
        
        if (col.CompareTag("Player"))
        {
            gameManager.IsOver();
            
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Bullet"))
        {
            if (pointsPoller == null)
                return;

            GameObject pointsGo = pointsPoller.GetObject();
            pointsGo.transform.position = transform.position;
            pointsGo.SetActive(true); 
            
            myPoller.PoolObject(gameObject);
        }
        else if (col.CompareTag("Ground"))
        {
            myPoller.PoolObject(gameObject);
        }
    }

    public override void NewRandomPosition()
    {
        transform.position = GetRandomPosition();
    }
}
