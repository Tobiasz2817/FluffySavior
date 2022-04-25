using UnityEngine;
using Random = UnityEngine.Random;

public class FallingObject : BasicObject
{
    private Poller myPoller;
    private Poller pointsPoller;

    private const int constSpeed = 6;
    private float lengthRounds = 0f;
    private int i = 1;
    void Start()
    {
        gameManager = GameManager.Instance;
        
        lengthRounds = gameManager.GetLengthRounds();
        
        myPoller = gameObject.GetComponentInParent<Poller>();

        pointsPoller = GameObject.Find("PollerPoints").GetComponent<Poller>();
        
        direction = Vector2.down;
        
    }

    private void OnEnable()
    {
        if (gameManager == null)
        {
            return;
        }
        
        if (randomSpeed)
        {
            i = gameManager.GetCurrentIndex();

            float min = i - 0.5f;
            float max = ((lengthRounds - 1) / 2) + i;
            speedObject = Random.Range(min, max);
        }
    }

    void Update()
    {
        if(isMove == true)
            transform.Translate(direction * Time.deltaTime * speedObject);
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

    public override void NewRandomPosition()
    {
        transform.position = GetRandomPosition();
    }
}
