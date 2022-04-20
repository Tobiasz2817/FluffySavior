using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    [SerializeField] 
    private List<int> compartmentResult;
    [SerializeField]
    private GameObject starsGo;

    private int finalStars = 0;

    private PointsCollector pointsCollector;
    private HorizontalLayoutGroup horizontalLayoutGroup;
    
    private List<GameObject> starsList = new List<GameObject>();
    private void Awake()
    {
        pointsCollector = FindObjectOfType<PointsCollector>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    private void OnEnable()
    {
        CalcFinalsScore();

        switch (finalStars)
        {
            case 1:
                SetPadding(120,120);
                break;
            case 2:
                SetPadding(100,100);
                break;
            case 3:
                SetPadding(60,60);
                break;
            case 4:
                SetPadding(20,20);
                break;
            case 5:
                SetPadding(0,0);
                break;
        }
        
        for (int i = 0; i < finalStars; i++)
        {
            GameObject star = Instantiate(starsGo, transform);
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
