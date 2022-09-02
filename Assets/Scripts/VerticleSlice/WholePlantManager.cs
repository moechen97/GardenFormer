using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholePlantManager : MonoBehaviour
{
    [SerializeField] private GameObject tinyCreature;
    [SerializeField] private int tinyCreatureAppearCount;

    public static event Action PlantsCanGrow  = delegate {};
    public static event Action PlantsCannotGrow  = delegate {};
    private int plantCount = 0;
    
    public static WholePlantManager instance { get; private set; }
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
    }
    
    private int TotalPlantCount = 0;

    [System.Serializable]
    public class Plant
    {
        public string type;
        public int State;
        public GameObject plant;
    }

    private List<Plant> plantedPlants = new List<Plant>();


    public void AddAPlant(string type, GameObject plant)
    {
        var newPlant = new Plant {type = type, plant = plant};

        plantedPlants.Add(newPlant);
    }

    public List<Plant> SendPlantsPosition()
    {
        if (plantedPlants.Count > 0)
        {
            return plantedPlants;
        }
        else
        {
            return null;
        }
    }

    public void AllowPlantsGrow()
    {
        PlantsCanGrow();
        Debug.Log("ths is called");
    }

    public void StopPlantsGrow()
    {
        PlantsCannotGrow();
    }

    public void AddOnePlant()
    {
        TotalPlantCount += 1;
        if (TotalPlantCount == tinyCreatureAppearCount)
        {
            tinyCreature.SetActive(true);
        }
    }
}
