using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP2 : MonoBehaviour
{
    [SerializeField] 
    private GameObject winContent;
    [SerializeField] 
    private GameObject loseContent;
    [SerializeField] 
    private GameObject pressAnyButton;

    private TimeController timeController;
    public static GameManagerP2 Instance { get; private set; }
    private void Awake()
    {
        pressAnyButton.SetActive(true);
        
        Instance = this;
        timeController = FindObjectOfType<TimeController>();
    }
    public void StartCountTime()
    {
        timeController.StartCountDownTimer();
    }

    public void StartGame()
    {
        pressAnyButton.SetActive(false);
    }
    public void WinPlanet()
    {
        winContent.SetActive(true);
        FindObjectOfType<EnemyControllerP2>().StopAllCoroutines();
        foreach (var enemy in FindObjectsOfType<EnemyShoot>())
        {
            enemy.StopAllCoroutines();
        }
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        /*LevelCompletedControl.unlockedMap = 3;*/
    }

    public void LosePlanet()
    {
        FindObjectOfType<EnemyControllerP2>().StopAllCoroutines();
        foreach (var enemy in FindObjectsOfType<EnemyShoot>())
        {
            enemy.StopAllCoroutines();
        }
        timeController.StopCountTime();
        loseContent.SetActive(true);
    }

    public int GetCurrentTime()
    {
        return timeController.GetActuallyTime();
    }
}
