using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    /*private int currentRound = 1;*/
    private int numberOfRounds;
    private int averageTimePerRound;

    private int i = 0;
    
    private float currentTime; 
    private float nextRoundTimer = 6f;
    private float roundDelay = 5f;
    
    private bool isSpawning = false;
    private bool isGameOver = false;
    private bool areNewRoundPause = false;

    private Coroutine loopRound;
    private Coroutine nextRound;
    private Coroutine polledObjectCoroutine;

    public static GameManager Instance { get; private set; }

    [SerializeField] private List<RoundController> roundController;
    [SerializeField] private TimeController timer;
    [SerializeField] private Poller myPoller;
    [SerializeField] private GameObject nextRoundGameObject;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        numberOfRounds = roundController.Count;
        averageTimePerRound = AverageTimePerRound();


        NextRound();
    }
    
    void Update()
    {
        if (!isGameOver)
        {
            currentTime = timer.GetActuallyTime();
            if (currentTime > (averageTimePerRound * (i + 1)))
            {
                i++;
                
                
                // Pause for 5 secound
               
                NextRound();
                
                
                if (i == roundController.Count)
                {
                    if(loopRound != null)
                        StopCoroutine(loopRound);
                    isGameOver = true;
                    return;
                }
            }

            if (!isSpawning && !areNewRoundPause)
            {
                loopRound = StartCoroutine(GamePlay(i));
                isSpawning = true;
            }
            Debug.Log($"Current time: {currentTime} AverageTimePerRound: {averageTimePerRound}");
        }
    }

    private IEnumerator DelayTimeNextRound()
    {
        yield return new WaitForSeconds(roundDelay);
        areNewRoundPause = false;
        nextRoundGameObject.SetActive(false);
        StopCoroutine(nextRound);
    }

    private IEnumerator GamePlay(int index)
    {
        List<GameObject> objects = roundController[index].GetObjectsList();
        foreach (GameObject gamePlayObjects in objects)
        {
            if (gameObject != null)
            {
                if (myPoller != null)
                {
                    float polledTime = 0f;
                    Type currentType = gamePlayObjects.GetComponent<BasicObject>().GetType();
                    if (currentType == typeof(FallingObject))
                    {
                        polledTime = Random.Range(0f, 4f);
                        Debug.Log("Falling Obj  time " + polledTime);
                    }
                    else if (currentType == typeof(IndestructibleObject))
                    {
                        polledTime = Random.Range(4f, 5.95f);
                        
                        Debug.Log("Indestrucbile time " + polledTime);
                    }

                    polledObjectCoroutine = StartCoroutine(PollObject(polledTime,currentType));
                }
            }
        }
        yield return new WaitForSeconds(nextRoundTimer);
        isSpawning = false;
    }

    private IEnumerator PollObject(float pollTime, Type type)
    {
        yield return new WaitForSeconds(pollTime);
        GameObject myGo = myPoller.GetObjectByType(type);
        if (myGo == null)
        {
            Debug.Log(" Null ");
            yield return null;
        }
        myGo.GetComponent<BasicObject>().NewRandomPosition();
        myGo.SetActive(true);
    }
    private void NextRound()
    {
        if(polledObjectCoroutine != null)
            StopCoroutine(polledObjectCoroutine);
        myPoller.PoolAllObject();
        
        nextRound = StartCoroutine(DelayTimeNextRound());

        if (roundDelay <= 5)
        {
            roundDelay = 10f;
        }
        
        nextRoundGameObject.SetActive(true);
        nextRoundGameObject.GetComponent<Text>().text = "Round " + (i + 1);
                
        areNewRoundPause = true;
    }
    public int AverageTimePerRound()
    {
        int endedtime = timer.GetEndedTime();

        return endedtime / numberOfRounds;
    }
}
