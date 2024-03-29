using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class IndestructibleObject : BasicObject
{
    private Poller myPoller;
    void Start()
    {
        myPoller = gameObject.GetComponentInParent<Poller>();
        
        gameManager = GameManager.Instance;
    }
    
    void Update()
    {
        if (isMove)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speedObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !gameManager.isGameOver)
        {
            gameManager.isDieByEnemy = true;
            gameManager.EndFirstPlanet();
            
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Bullet") && !col.transform.parent.CompareTag("MovingObjBullet"))
        {
            col.gameObject.GetComponentInParent<Poller>().PoolObject(col.gameObject);
        }
        else if (col.CompareTag("Ground"))
        {
            myPoller.PoolObject(gameObject);
        }
    }

    public override void NewRandomPosition()
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth,camera.pixelHeight));

        float leftSide = Random.Range((pos.x * -1), (pos.x * -1) + 5);
        float rightSide = Random.Range(pos.x, (pos.x) - 5);
        float middleSide = Random.Range((pos.x * -1) + 5, (pos.x) - 5);
        
        transform.position = new Vector2(GetRandomVarriable(leftSide,middleSide,rightSide),pos.y + 8f);
    }

    private float GetRandomVarriable(params float[] floatsVarriable)
    {
        if (floatsVarriable == null || floatsVarriable.Length == 0) return 0f;
        
        int randomVar = Random.Range(0, floatsVarriable.Length);
        return floatsVarriable[randomVar];
    }
}
