using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoundController 
{
    [SerializeField] private List<GameObject> objects;
    
    public List<GameObject> GetObjectsList()
    {

        return objects;
    }
}
