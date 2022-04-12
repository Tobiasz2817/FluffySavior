using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poller : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;
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
        GameObject createdObj = Instantiate(poolObject, transform);
        createdObj.SetActive(false);
        myObjects.Add(createdObj);
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < myObjects.Count; i++)
        {
            if (!myObjects[i].activeInHierarchy)
            {
                StartCoroutine(PoolAfterTime(myObjects[i]));
                return myObjects[i];
            }
        }

        return null;
    }

    private IEnumerator PoolAfterTime(GameObject poolObject)
    {
        yield return new WaitForSeconds(objectDisappearingTime);
        PoolObject(poolObject);
    }
    public void PoolObject(GameObject poolObj)
    {
        if (myObjects.Contains(poolObj))
        {
            poolObj.SetActive(false);
            Debug.Log(" Pooling obj ");
        }
    }

    public float GetBalancedFireRate()
    {
        return objectDisappearingTime / countObj;
    }
}
