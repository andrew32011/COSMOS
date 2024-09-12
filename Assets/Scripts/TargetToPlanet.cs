using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetToPlanet : MonoBehaviour
{
    [SerializeField] public GameObject planetCenter;
    
    void Update()
    {
        transform.right = planetCenter.transform.position - transform.position;
    }
}
