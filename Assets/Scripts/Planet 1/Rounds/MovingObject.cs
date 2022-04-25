using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MovingObject : BasicObject
{
    private Poller myPoller;
    private Poller pointsPoller;
    private Poller bulletPoller;

    private Transform bulletPoint;

    private float fireRate = 2f;
    private float speedBullet = 1000f;
    private float cameraX;
    
    private bool isAttack = false;
    public bool isMoveDown = false;

    private Vector2 directionMove;
    
    void Start()
    {
        gameManager = GameManager.Instance;
        
        Camera camera = Camera.main;
        cameraX = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight)).x;
        
        myPoller = gameObject.GetComponentInParent<Poller>();
        bulletPoint = transform.GetChild(0).transform;
            
        if (randomSpeed)
        {
            speedObject = Random.Range(4f, 6f);
        }
        
        pointsPoller = GameObject.Find("PollerPoints").GetComponent<Poller>();
        bulletPoller = GameObject.Find("PollerMovingBullet").GetComponent<Poller>();
        
        direction = Vector2.down;

        int dr = 0;
        while (dr == 0)
        {
            dr = Random.Range(-1, 1);
        }
        directionMove = new Vector2(dr,0);
    }
    void Update()
    {
        if (isMove == true)
        {
            if (isMoveDown)
            {
                if(directionMove != Vector2.down)
                    directionMove = Vector2.down;
            }
            else
            {
                if (transform.position.x > cameraX - 2f)
                {
                    directionMove = Vector2.left;
                }
                else if(transform.position.x < (cameraX * -1) + 2f)
                {
                    directionMove = Vector2.right;
                }
            }
            
            transform.Translate(directionMove * Time.deltaTime * speedObject);
        }

        if (!isAttack)
        {
            GameObject shootPrefab = bulletPoller.GetObject();
            
            if (shootPrefab == null) return;
            
            shootPrefab.transform.position = bulletPoint.position;
            shootPrefab.transform.rotation = bulletPoint.rotation;

            Rigidbody2D rbR2 = shootPrefab.GetComponent<Rigidbody2D>();
            rbR2.velocity = Vector2.zero;
            
            shootPrefab.SetActive(true);
            
            rbR2.AddForce(direction * speedBullet * Time.fixedDeltaTime , ForceMode2D.Impulse);
            
            isAttack = true;
            Invoke(nameof(ShootDelayTime),fireRate);
        }
    }
    private void ShootDelayTime()
    {
        isAttack = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !gameManager.isGameOver)
        {
            gameManager.isDieByEnemy = true;
            gameManager.EndFirstPlanet();
            
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Bullet") && !col.transform.parent.CompareTag("MovingObjBullet") && !gameManager.isGameOver)
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

    public void MoveDown()
    {
        if (gameManager.isGameOver)
        {
            isMoveDown = true;
        }
    }

    public override void NewRandomPosition()
    {
        transform.position = GetRandomPosition();
    }
}
