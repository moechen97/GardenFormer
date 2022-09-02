using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantFullGrowFlowerState : PlantBaseState
{
    private PlantStateManager plant;
    private GameObject flower;
    private float currentTime = 0;

    private WholePlantManager _wholePlantManager;
    private bool isCounted = false;
    
    
    public override void GrowState(PlantStateManager plant)
    {
        Vector3 flowerRotation = new Vector3(0, Random.Range(0f, 360f), Random.Range(0f, 360f));
        flower = GameObject.Instantiate(plant.Flower, plant.FlowerGeneratePosition.position, quaternion.Euler(flowerRotation),
            plant.transform);
        flower.transform.localScale = flower.transform.localScale + Vector3.one * Random.Range(0, 0.2f);
        currentTime = Time.time;

        _wholePlantManager = GameObject.FindObjectOfType<WholePlantManager>();
        if (!isCounted)
        {
            _wholePlantManager.AddOnePlant();
            isCounted = true;
        }

    }

    public override void UpdateState(PlantStateManager plant)
    {
        if (Time.time > currentTime + plant.Florescence)
        {
            flower.GetComponent<FlowerPadelGenerator>().FlowerDisappear();
            plant.SwitchStates(plant.FullGrowState);
        }
    }

    public override void EndState(PlantStateManager plant)
    {
        
    }
}
