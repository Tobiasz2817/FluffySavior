using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject canPlay;
    [SerializeField] private GameObject cantPlay;

    public void TryPlayButton(string currentIndexMap)
    {
        if (LevelCompletedControl.unlockedMap <= int.Parse(currentIndexMap))
        {
            canPlay.SetActive(true);
            
            return;
        }

        cantPlay.SetActive(true);
    }
}
