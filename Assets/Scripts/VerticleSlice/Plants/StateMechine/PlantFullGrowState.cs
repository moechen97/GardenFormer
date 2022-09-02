using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFullGrowState : PlantBaseState
{
    private float waitingTime = 0;
    
    
    
   public override void GrowState(PlantStateManager plant)
   {
       waitingTime = Time.time + plant.FlowerGapTime + Random.Range(0,4);
   }

    public override void UpdateState(PlantStateManager plant)
    {
        if (Time.time > waitingTime)
        {
            plant.SwitchStates(plant.FullGrowFlowerState);
        }
    }

    public override void EndState(PlantStateManager plant)
    {
        
    }
}
