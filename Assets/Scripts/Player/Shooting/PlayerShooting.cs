using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerShooting : MonoBehaviour
{
    public enum Weapon
    {
        oneShot = 1,
        twoShot = 2,
        threeShot = 3
    }
    
    [SerializeField]
    private Poller poller;

    [SerializeField] 
    private Transform leftBulletPoint;
    [SerializeField]
    private Transform rightBulletPoint;
    [SerializeField]
    private Transform middleBulletPoint;
    
    [SerializeField] 
    private float speedBullet;

    private float fireRate = 0.15f;

    private bool isAttack = false;

    public Weapon myWeapon;

    private void Start()
    {
        fireRate = poller.GetBalancedFireRate();

        myWeapon = Weapon.oneShot;
    }

    public void ShootImpulseCallback(InputAction.CallbackContext hitKey)
    {
        if (hitKey.action.triggered && !isAttack)
        {
            switch (myWeapon)
            {
                case Weapon.oneShot:
                    Shoot(middleBulletPoint,Vector2.up);
                    break;
                
                case Weapon.twoShot:
                    Shoot(leftBulletPoint,Vector2.up);
                    Shoot(rightBulletPoint,Vector2.up);
                    break;
                
                case Weapon.threeShot:
                    Shoot(leftBulletPoint,Vector2.up);
                    Shoot(middleBulletPoint,Vector2.up);
                    Shoot(rightBulletPoint,Vector2.up);
                    break;
            }

            isAttack = true;
            Invoke(nameof(ShootDelayTime),fireRate);
        }
    }

    private void Shoot(Transform bulletPoint, Vector2 direction)
    {
        GameObject shootPrefab = poller.GetObject();
            
        if (shootPrefab == null) return;
            
        shootPrefab.transform.position = bulletPoint.position;
        shootPrefab.transform.rotation = bulletPoint.rotation;

        Rigidbody2D rbR2 = shootPrefab.GetComponent<Rigidbody2D>();
        rbR2.velocity = Vector2.zero;
            
        shootPrefab.SetActive(true);
            
        rbR2.AddForce(direction * speedBullet * Time.fixedDeltaTime , ForceMode2D.Impulse);
    }
    private void ShootDelayTime()
    {
        isAttack = false;
    }

    public void UpgradeWeapon()
    {
        myWeapon = myWeapon + 1;
    }
}
