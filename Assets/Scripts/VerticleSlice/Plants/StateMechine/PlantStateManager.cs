using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateManager: MonoBehaviour
{
    [SerializeField] private PlantPreset plantPreset;
    [SerializeField] private Transform flowerPosition;

    private bool canUpdate = false;
    
    //state Machine
    private PlantBaseState currentState;
    [HideInInspector] public PlantGrowingState GrowingState = new PlantGrowingState();
    [HideInInspector] public PlantFullGrowState FullGrowState = new PlantFullGrowState();
    [HideInInspector] public PlantFullGrowFlowerState FullGrowFlowerState = new PlantFullGrowFlowerState();
    [HideInInspector] public GameObject Flower ;
    [HideInInspector] public float GrowUpSpeed;
    [HideInInspector] public float Florescence;
    [HideInInspector] public float Length;
    [HideInInspector] public float InitialHeight;
    [HideInInspector] public float FlowerGapTime;
    [HideInInspector] public float RandomHeightGap;
    [HideInInspector] public Transform FlowerGeneratePosition;

    void Awake()
    {
        Flower = plantPreset.Flower;
        GrowUpSpeed = plantPreset.GrowUpSpeed;
        Florescence = plantPreset.Florescence;
        Length = plantPreset.Length;
        InitialHeight = plantPreset.InitialHeight;
        FlowerGapTime = plantPreset.FlowerGapTime;
        RandomHeightGap = plantPreset.RandomHeightGap;
        FlowerGeneratePosition = flowerPosition;
        
        currentState = GrowingState;
        
        currentState.GrowState(this);

        WholePlantManager.PlantsCanGrow += PlantCanGrow;
        WholePlantManager.PlantsCannotGrow += PlantCannotGrow;
    }

    private void OnDisable()
    {
        WholePlantManager.PlantsCanGrow -= PlantCanGrow;
        WholePlantManager.PlantsCannotGrow -= PlantCannotGrow;
    }

    private void OnDestroy()
    {
        WholePlantManager.PlantsCanGrow -= PlantCanGrow;
        WholePlantManager.PlantsCannotGrow -= PlantCannotGrow;
    }

    private void PlantCannotGrow()
    {
        canUpdate = false;
    }

    void Update()
    {
        if (!canUpdate)
            return;
        currentState.UpdateState(this);
        Debug.Log( "growing" + currentState);
    }

    public void SwitchStates(PlantBaseState state)
    {
        currentState = state;
        state.GrowState(this);
    }

    private void PlantCanGrow()
    {
        canUpdate = true;
    }

}
