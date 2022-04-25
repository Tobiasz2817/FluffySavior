using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    private int finalStars = 0;

    private PointsCollector pointsCollector;
    private HorizontalLayoutGroup horizontalLayoutGroup;
    
    private List<GameObject> starsList = new List<GameObject>();

    private GameManager gameManager;
    [SerializeField] 
    private List<int> compartmentResult;
    [SerializeField]
    private GameObject starsGo;
    [SerializeField] 
    private Transform parent;
    
    [SerializeField] private GameObject winContent;
    [SerializeField] private GameObject loseContent;


    private void Awake()
    {
        pointsCollector = FindObjectOfType<PointsCollector>();
        horizontalLayoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();
    }

    private void OnEnable()
    {
        if(gameManager == null)
            gameManager = GameManager.Instance;
        
        CalcFinalsScore();

        switch (finalStars)
        {
            case 1:
                SetPadding(320,320);
                break;
            case 2:
                SetPadding(240,240);
                break;
            case 3:
                SetPadding(180,180);
                break;
            case 4:
                SetPadding(140,140);
                break;
            case 5:
                SetPadding(100,100);
                break;
        }

        ResultNumberOfStars();
        
        for (int i = 0; i < finalStars; i++)
        {
            GameObject star = Instantiate(starsGo, parent);
            starsList.Add(star);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < starsList.Count; i++)
        {
            Destroy(starsList[i].gameObject);
        }
        starsList.Clear();
    }

    private void ResultNumberOfStars()
    {
        while (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        
        if (finalStars >= 3 && !gameManager.isDieByEnemy)
        {
            winContent.SetActive(true);
            LevelCompletedControl.unlockedMap = 2;
            return;
        }
        
        loseContent.SetActive(true);
    }
    private void SetPadding(int left, int right)
    {
        horizontalLayoutGroup.padding.left = left;
        horizontalLayoutGroup.padding.right = right;
    }
    private void CalcFinalsScore()
    {
        float points = pointsCollector.Points;


        for (int i = compartmentResult.Count - 1; i >= 0; i--)
        {
            if (points < compartmentResult[i])
            {
                continue;
            }
            
            finalStars = i + 1;
            break;
        }

    }
}
