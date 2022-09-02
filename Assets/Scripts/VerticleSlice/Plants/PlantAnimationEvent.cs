using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] plantComponents;

    void Part1Active()
    {
        if(plantComponents[0]!=null)
            plantComponents[0].SetActive(true);
    }
    
    void Part2Active()
    {
        if(plantComponents[1]!=null)
            plantComponents[1].SetActive(true);
    }
    
    void Part3Active()
    {
        if(plantComponents[2]!=null)
            plantComponents[2].SetActive(true);
    }
}
