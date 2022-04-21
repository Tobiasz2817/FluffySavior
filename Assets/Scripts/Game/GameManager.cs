using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    /*private int currentRound = 1;*/
    private int numberOfRounds;
    private float averageTimePerRound;

    private int i = 0;
    
    private float currentTime;
    private float nextRoundLostTime = 0f; 
    private float nextRoundTimer = 6f;
    private float roundDelay = 5f;
    private float regulateTime = 0f;
    
    private bool isSpawning = false;
    private bool areNewRoundPause = false;
    
    [HideInInspector]
    public bool isGameOver = false;

    private Coroutine loopRound;
    private Coroutine nextRound;
    private Coroutine delayTime;
    private Coroutine polledObjectCoroutine;

    public static GameManager Instance { get; private set; }

    [SerializeField] private List<RoundController> roundController;
    [SerializeField] private TimeController timer;
    [SerializeField] private Poller myPoller;
    [SerializeField] private GameObject nextRoundGameObject;
    [SerializeField] private List<Sprite> currentNumberRoundSprites;

    [SerializeField] private GameObject resultEndGame;
    
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
            if (currentTime > (nextRoundLostTime + (averageTimePerRound * (i + 1)) + regulateTime))
            {
                i++;
                
                if(numberOfRounds > 1)
                    nextRoundTimer -= i;
                
                if (i == roundController.Count)
                {
                    EndFirstPlanet();
                    return;
                }

                NextRound();

            }

            if (!isSpawning && !areNewRoundPause)
            {
                loopRound = StartCoroutine(GamePlay(i));
                isSpawning = true;
            }
            /*Debug.Log($"Current time: {currentTime} AverageTimePerRound: {averageTimePerRound + nextRoundLostTime}");
            Debug.Log(" lost time " + nextRoundLostTime);*/
            /*Debug.Log(" Number of rounds " + nextRoundTimer);*/
        }
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
                    Type currentType = gamePlayObjects.GetComponent<BasicObject>().GetType();
                    float polledTime = GetTimePolledByType(currentType);
                        
                    /*Debug.Log(" Type " + currentType.Name + " Obj  time " + polledTime);*/
                    
                    polledObjectCoroutine = StartCoroutine(PollObject(polledTime,currentType));
                }
            }
        }
        yield return new WaitForSeconds(nextRoundTimer);
        isSpawning = false;
    }

    private float GetTimePolledByType(Type type)
    {
        switch (type.ToString())
        {
            case "FallingObject":
                return Random.Range(0f, 4f);
            case "IndestructibleObject":
                return Random.Range(4f, 5.95f);
        }
        return 0f;
    }
    private IEnumerator PollObject(float pollTime, Type type)
    {
        yield return new WaitForSeconds(pollTime);
        GameObject myGo = myPoller.GetObjectByType(type);
        if (myGo == null)
        {
            yield return null;
        }

        if (type == typeof(IndestructibleObject))
        {
            myGo.GetComponent<IndestructibleObject>().IsMove = false;
        }
        myGo.GetComponent<BasicObject>().NewRandomPosition();
        myGo.SetActive(true);
    }
    private void NextRound()
    {
        StopAllCoroutines();
        myPoller.PoolAllObject();

        nextRoundLostTime = 0f;
        nextRound = StartCoroutine(WaitForEmptyScene());
        
        isSpawning = false;
    }

    private IEnumerator WaitForEmptyScene()
    {
        areNewRoundPause = true;
        myPoller.SpeedObjectOnEndRound();
        while (true)
        {
            if (!myPoller.IsSomethingOnScene())
            {
                float value = 0f;
                value = nextRoundLostTime / 100;
                
                timer.AddTimeToEnd(value);
                
                nextRoundGameObject.SetActive(true);
                
                if(i < currentNumberRoundSprites.Count)
                    nextRoundGameObject.transform.GetChild(0).GetComponent<Image>().sprite = currentNumberRoundSprites[i];

                delayTime = StartCoroutine(DelayTimeNextRound());
                if(nextRound != null)
                    StopCoroutine(nextRound);
                break;
            }

            nextRoundLostTime += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator DelayTimeNextRound()
    {
        yield return new WaitForSeconds(roundDelay);
        areNewRoundPause = false;
        nextRoundGameObject.SetActive(false);
        
        if(delayTime != null)
            StopCoroutine(delayTime);
    }

    public int AverageTimePerRound()
    {
        int endedtime = timer.GetEndedTime();

        return endedtime / numberOfRounds;
    }
    private void StopGame()
    {
        StopAllCoroutines();
        isGameOver = true;
    }
    public void EndFirstPlanet()
    {
        StopGame();
        
        if(nextRoundGameObject.activeInHierarchy)
            nextRoundGameObject.SetActive(false);
        
        Camera.main.gameObject.GetComponent<UpperMoveCamera>().SetSpeedCamera(0f);
        myPoller.SpeedObjectOnEndRound();
        
        resultEndGame.SetActive(true);
    }

    public int GetCurrentIndex()
    {
        return i;
    }

    public int GetLengthRounds()
    {
        return roundController.Count;
    }
}
