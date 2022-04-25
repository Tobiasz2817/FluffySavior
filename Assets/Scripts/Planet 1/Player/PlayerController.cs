using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private bool gameStarted = false;
    
    [SerializeField] private GameObject loseContent;
    public void Start()
    {
        gameManager = GameManager.Instance;

        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void StartGame(InputAction.CallbackContext press)
    {
        if (press.action.triggered && !gameStarted)
        {
            gameManager.StartGame();
            gameStarted = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("UpperGround"))
        {
            gameManager.isDieByEnemy = true;
            gameManager.EndFirstPlanet();
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            
            Destroy(gameObject);
        }
        else if (col.CompareTag("Key"))
        {
            Destroy(col.gameObject);
            GetComponent<PlayerShooting>().UpgradeWeapon();
        }
        
    }
}
 