using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Threading;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Poller myBulletPoller;
    private void Start()
    {
        myBulletPoller = gameObject.GetComponentInParent<Poller>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (myBulletPoller == null) return;

        if (col.CompareTag("UpperGround"))
        {
            myBulletPoller.PoolObject(gameObject);
        }
        else if (col.CompareTag("Ground"))
        {
            myBulletPoller.PoolObject(gameObject);
        }
        else if (col.CompareTag("FallingObj") && !myBulletPoller.CompareTag("MovingObjBullet"))
        {
            myBulletPoller.PoolObject(gameObject);
        }
        else if (col.CompareTag("MovingObj") && !myBulletPoller.CompareTag("MovingObjBullet"))
        {
            myBulletPoller.PoolObject(gameObject);
        }
        
        if (myBulletPoller.CompareTag("MovingObjBullet") && col.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.isDieByEnemy = true;
            gameManager.EndFirstPlanet();
            Destroy(col.gameObject);
        }
        
    }
}
