using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantGrowingState : PlantBaseState
{
    private PlantStateManager _plant;
    private float growSpeed;
    private float currentHeight = 0;
    private float finalHeight = 0;
    private float lerpContent = 0;
    
    public override void GrowState(PlantStateManager plant)
    {
        _plant = plant;
        growSpeed = plant.GrowUpSpeed;
        _plant.transform. GetChild(0).localScale = new Vector3(1, _plant.InitialHeight, 1);
        finalHeight = 1 + Random.Range(0,_plant.RandomHeightGap);
        currentHeight = _plant.InitialHeight;

    }

    public override void UpdateState(PlantStateManager plant)
    {
        if (currentHeight < finalHeight)
        {
            lerpContent += growSpeed * Time.deltaTime;
            currentHeight = Mathf.Lerp(_plant.InitialHeight, finalHeight, lerpContent);
            _plant.transform. GetChild(0).localScale = new Vector3(1, currentHeight, 1);
        }
        else
        {
            plant.SwitchStates(plant.FullGrowState);
        }
        
    }

    public override void EndState(PlantStateManager plant)
    {
        
    }
}
