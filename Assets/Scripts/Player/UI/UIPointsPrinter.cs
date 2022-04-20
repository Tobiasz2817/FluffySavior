using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointsPrinter : MonoBehaviour
{
    private Text pointText;
    private PointsCollector pointsCollector;
    void Start()
    {
        pointsCollector = FindObjectOfType<PointsCollector>();
        pointsCollector.PrinterValueEvent += PrintPoints;
        
        pointText = GetComponent<Text>();

        pointText.text = "Points: 0";
    }

    public void PrintPoints(float points)
    {
        pointText.text = "Points: " + points;
    }
}
