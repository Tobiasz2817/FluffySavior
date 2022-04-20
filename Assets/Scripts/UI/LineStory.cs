using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineStory : MonoBehaviour
{
    public static int controlNumber = 0;
    void Start()
    {
        if (controlNumber == 0)
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
