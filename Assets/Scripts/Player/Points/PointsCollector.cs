using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollector : MonoBehaviour
{
    public delegate void PrinterValue(float value);
    public PrinterValue PrinterValueEvent;

    private float points;
    public float Points
    {
        set => points = value;
        get => points;
    }
    public void EarnPoint(float valuePoint)
    {
        points += valuePoint;

        
        
        PrinterValueEvent?.Invoke(points);
    }
    
    
}
