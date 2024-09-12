using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    public static float Points;
    [SerializeField] TextMeshProUGUI textPoints;
    private void Start()
    {
        Points = 1000;
        textPoints.text = Points.ToString();
    }
    public void AddPoints(float points)
    {
        Points += points;
        textPoints.text = Points.ToString();
    }
    
}
