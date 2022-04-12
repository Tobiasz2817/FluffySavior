using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    /*private int currentRound = 1;*/
    private int numberOfRounds;
    private int averageTimePerRound;

    private int i = 0;
    
    private float currentTime; 
    private float nextRoundTimer = 6f;
    
    private bool isSpawning = false;
    private bool isGameOver = false;

    private Coroutine loopRound;
    
    [SerializeField] private List<RoundController> roundController;
    [SerializeField] private TimeController timer;
    
    void Start()
    {
        numberOfRounds = roundController.Count;
        averageTimePerRound = AverageTimePerRound();
    }
    
    void Update()
    {
        if (!isGameOver)
        {
            currentTime = timer.GetActuallyTime();
            if (currentTime > (averageTimePerRound * (i + 1)))
            {
                i++;

                if (i == roundController.Count)
                {
                    StopCoroutine(loopRound);
                    isGameOver = true;
                    return;
                }
            }

            if (!isSpawning)
            {
                loopRound = StartCoroutine(GamePlay(i));
                isSpawning = true;
            }
            Debug.Log($"Current time: {currentTime} AverageTimePerRound: {averageTimePerRound}");
        }
    }

    private IEnumerator GamePlay(int index)
    {
        List<GameObject> objects = roundController[index].GetObjectsList();
        foreach (GameObject gameObject in objects)
        {
            if (gameObject != null)
            {
                Instantiate(gameObject);
            }
        }
        yield return new WaitForSeconds(nextRoundTimer);
        isSpawning = false;
    }
    public int AverageTimePerRound()
    {
        int endedtime = timer.GetEndedTime();

        return endedtime / numberOfRounds;
    }
}
