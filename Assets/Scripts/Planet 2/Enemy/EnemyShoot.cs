using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    
    [SerializeField] 
    private float speedBullet = 2f;
    
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform playerTransform;
    
    private float randomFireRate;

    private TimeController timeController;
    private Poller myPoller;
    private GameManagerP2 gameManagerP2;

    private Coroutine shootCoroutine;
    void Start()
    {
        gameManagerP2 = GameManagerP2.Instance;
        timeController = FindObjectOfType<TimeController>();
        GameObject bulletPoller = GameObject.FindWithTag("EnemyBulletPoller");
        if (bulletPoller == null)
        {
            Debug.Log(" Bullet Poller didnt exist check the correct name tag");
        }    
            
        myPoller = bulletPoller.GetComponent<Poller>();
    }

    public void InvokeShooting()
    {
        shootCoroutine = StartCoroutine(GamePlay());
    }
    IEnumerator GamePlay()
    {
        while (true)
        {
            if (playerTransform == null)
            {
                StopCoroutine(shootCoroutine);
                yield return null;
            }


            if (gameManagerP2.GetCurrentTime() <= 30f)
            {
                speedBullet = 500f;
            }
            else if (gameManagerP2.GetCurrentTime() <= 60f)
            {
                speedBullet = 600f;
            }
            else
            {
                speedBullet = 700f;
            }
            randomFireRate = Random.Range(2f, 5f);

            Vector2 direction = playerTransform.position - transform.position;
            Shoot(bulletPoint,direction);
            

            yield return new WaitForSeconds(randomFireRate);
        }
    }
    private void Shoot(Transform bulletPoint, Vector2 direction)
    {
        GameObject shootPrefab = myPoller.GetObject();
            
        if (shootPrefab == null) return;
            
        shootPrefab.transform.position = bulletPoint.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        shootPrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
        Rigidbody2D rbR2 = shootPrefab.GetComponent<Rigidbody2D>();
        rbR2.velocity = Vector2.zero;
            
        shootPrefab.SetActive(true);
        rbR2.AddForce(direction.normalized * speedBullet * Time.fixedDeltaTime , ForceMode2D.Impulse);
    }
    

}
