using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poller : MonoBehaviour
{
    [SerializeField] private List<GameObject> poolObject = new List<GameObject>();
    [SerializeField] private int countObj;

    [SerializeField]
    private float objectDisappearingTime = 3f;

    private List<GameObject> myObjects = new List<GameObject>();
    void Awake()
    {
        for (int i = 0; i < countObj; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        for (int i = 0; i < poolObject.Count; i++)
        {
            GameObject createdObj = Instantiate(poolObject[i], transform);
            createdObj.SetActive(false);
            myObjects.Add(createdObj);
        }
        
        
    }

    public GameObject GetObjectByType(Type basicObject)
    {
        for (int i = 0; i < myObjects.Count; i++)
        {
            if(basicObject == myObjects[i].GetComponent<BasicObject>().GetType())
            {
                if (!myObjects[i].activeInHierarchy)
                {
                    return myObjects[i];
                }
            }
        }

        return null;
    }
    public GameObject GetObject()
    {
        for (int i = 0; i < myObjects.Count; i++)
        {
            if (!myObjects[i].activeInHierarchy)
            {
                return myObjects[i];
            }
        }

        return null;
    }
    public void PoolObject(GameObject poolObj)
    {
        if (myObjects.Contains(poolObj))
        {
            poolObj.SetActive(false);
        }
    }

    public void PoolAllObject()
    {
        int delayCheckGoActive = 0;
        Camera camera = Camera.main;
        Vector3 pos = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth,camera.pixelHeight));
        for (int i = 0; i < myObjects.Count; i++)
        {
            if (myObjects[i].activeInHierarchy)
            {
                if (myObjects[i].transform.position.y > pos.y)
                {
                    myObjects[i].SetActive(false);
                }
                if(delayCheckGoActive > 0)
                    delayCheckGoActive--;
            }
            else
            {
                delayCheckGoActive++;
            }

            if (delayCheckGoActive > 10)
            {
                break;
            }
        }
    }

    public float GetBalancedFireRate()
    {
        return objectDisappearingTime / countObj;
    }
}
