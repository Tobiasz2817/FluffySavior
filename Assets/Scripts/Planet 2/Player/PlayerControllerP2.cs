using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerP2 : MonoBehaviour
{
    private bool gameStarted = false;
    private GameManagerP2 gameManagerP2;
    private Rigidbody2D rigidbody2D;
    void Start()
    {
        gameManagerP2 = GameManagerP2.Instance;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0f;
    }
    
    public void StartGame(InputAction.CallbackContext press)
    {
        if (press.action.triggered && !gameStarted)
        {
            rigidbody2D.gravityScale = 1f;
            gameManagerP2.StartCountTime();
            gameManagerP2.StartGame();
            FindObjectOfType<EnemyControllerP2>().StartGame();
            gameStarted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("UpperGround"))
        {
            gameManagerP2.LosePlanet();
            Destroy(gameObject);
        }
        else if (col.CompareTag("Bullet"))
        {
            gameManagerP2.LosePlanet();
            Destroy(gameObject);
        }
    }
}
