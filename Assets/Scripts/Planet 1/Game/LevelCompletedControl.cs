using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedControl : MonoBehaviour
{
    public static int unlockedMap = 1;
    public void SaveObject()
    {
        DontDestroyOnLoad(gameObject);
    }
}
