using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControl : MonoBehaviour
{
    private Poller myPoller;
    private PointsCollector pointsCollector;
    private PointSprite pointSprite;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private float valuePoint = 15f;

    private void Awake()
    {
        pointSprite = FindObjectOfType<PointSprite>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        myPoller = GetComponentInParent<Poller>();

        pointsCollector = FindObjectOfType<PointsCollector>();
    }

    private void OnEnable()
    {
        if(spriteRenderer == null)
            Debug.Log(" Spiret null ");
        else if(pointSprite == null)
            Debug.Log("Point spruite null");
        
        spriteRenderer.sprite = pointSprite.GetRandomSprite(spriteRenderer.sprite);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!pointsCollector && !myPoller)
            {
                return;
            }
            
            pointsCollector.EarnPoint(valuePoint);
            myPoller.PoolObject(gameObject);
        }
        else if (col.gameObject.CompareTag("Ground"))
        {
            myPoller.PoolObject(gameObject);
        }
    }
}
