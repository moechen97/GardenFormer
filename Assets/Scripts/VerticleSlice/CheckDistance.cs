using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertexAnimationTools_30;

public class CheckDistance : MonoBehaviour
{
    [SerializeField] private Color canPlantUnhealthyColor;
    [SerializeField] private Color cannotPlantColor;
    [SerializeField] private Color canPlantHealthyColor;
    [SerializeField] private float CannotPlantDistance;
    [SerializeField] private float FullGrowDistance;
    [SerializeField] private MeshSequencePlayer _sequencePlayer;

    private bool canPlant = true;

    private float nearestPlantDistance = 100;
    
    private List<WholePlantManager.Plant> plantsList;

    public enum plantState
    {
        healthy,
        unhealthy,
        cannotbePlanted
    }

    private plantState _state;


    void Start()
    {
        plantsList = WholePlantManager.instance.SendPlantsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (plantsList != null)
        {
            nearestPlantDistance = 100;
            foreach (var plant in plantsList)
            {
                float plantsDistance = Vector3.Distance(transform.position, plant.plant.transform.position);
                if (plantsDistance < nearestPlantDistance)
                {
                    nearestPlantDistance = plantsDistance;
                }
                
                if (plantsDistance < CannotPlantDistance)
                {
                    //change color
                    transform.GetChild(0).GetComponent<Renderer>().material.color = cannotPlantColor;
                    _state = plantState.cannotbePlanted;
                    _sequencePlayer.NormalizedTime =  1f;
                    canPlant = false;
                    PlantButton.instance.SetButtonUnInteractable();
                    break;
                }
                else
                {
                    PlantButton.instance.SetButtonInteractable();
                    canPlant = true;
                }
                
            }

            if (!canPlant)
            {
                return;
            }

            if (nearestPlantDistance < FullGrowDistance)
            {
                //change color
                transform.GetChild(0).GetComponent<Renderer>().material.color = canPlantUnhealthyColor;
                _sequencePlayer.NormalizedTime = 0.5f;
                _state = plantState.unhealthy;
            }
            else
            {
                //change color
                transform.GetChild(0).GetComponent<Renderer>().material.color = canPlantHealthyColor;
                _sequencePlayer.NormalizedTime =  0f;
                _state = plantState.healthy;
            }
            
        }
    }

    public int getPlantState()
    {
        return (int) _state;
    }
}
