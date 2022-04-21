using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] private GameObject loseContent;
    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("UpperGround"))
        {
            gameManager.EndFirstPlanet();
            
            Destroy(gameObject);
        }
        else if (col.CompareTag("Key"))
        {
            GetComponent<PlayerShooting>().UpgradeWeapon();
            Destroy(col.gameObject);
        }
        
    }
}
 