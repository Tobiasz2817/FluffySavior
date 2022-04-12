using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Poller poller;

    [SerializeField] 
    private Transform bulletPoint;

    [SerializeField] 
    private float speedBullet;

    private float fireRate = 0.15f;

    private bool isAttack = false;

    private void Start()
    {
        fireRate = poller.GetBalancedFireRate();
    }

    public void ShootImpulseCallback(InputAction.CallbackContext hitKey)
    {
        if (hitKey.action.triggered && !isAttack)
        {
            GameObject shootPrefab = poller.GetObject();
            
            if (shootPrefab == null) return;
            
            shootPrefab.transform.position = bulletPoint.position;
            shootPrefab.transform.rotation = bulletPoint.rotation;

            Rigidbody2D rbR2 = shootPrefab.GetComponent<Rigidbody2D>();
            rbR2.velocity = Vector2.zero;
            
            shootPrefab.SetActive(true);
            
            rbR2.AddForce(Vector2.up * speedBullet * Time.fixedDeltaTime , ForceMode2D.Impulse);

            Invoke(nameof(ShootDelayTime),fireRate);
            isAttack = true;
        }
    }

    private void ShootDelayTime()
    {
        isAttack = false;
    }
}
