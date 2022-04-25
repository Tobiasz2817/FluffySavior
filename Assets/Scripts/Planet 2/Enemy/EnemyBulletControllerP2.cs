using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class EnemyBulletControllerP2 : MonoBehaviour
{
    private Poller myPoller;
    
    void Start()
    {
        myPoller = GetComponentInParent<Poller>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("UpperGround"))
        {
            myPoller.PoolObject(gameObject);
        }
    }
}
