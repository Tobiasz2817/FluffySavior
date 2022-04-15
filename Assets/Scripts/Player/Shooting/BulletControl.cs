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
            
            Debug.Log("Booom o podłoge !");
        }
        else if (col.CompareTag("FallingObj"))
        {
            myBulletPoller.PoolObject(gameObject);
        }
    }
}
