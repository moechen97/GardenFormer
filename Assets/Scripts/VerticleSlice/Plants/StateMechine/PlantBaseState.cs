
using UnityEngine;

public abstract class PlantBaseState
{
   public abstract void GrowState(PlantStateManager plant);

   public abstract void UpdateState(PlantStateManager plant);

   public abstract void EndState(PlantStateManager plant);
   
}
