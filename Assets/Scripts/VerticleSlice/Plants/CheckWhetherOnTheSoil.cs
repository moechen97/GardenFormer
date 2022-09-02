using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckWhetherOnTheSoil : MonoBehaviour
{
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float cannotPLantDistance;
    
    public enum plantType
    {
        SphereFlower,
        BoxFlower,
        CylinderFlower,
        Grass
    };
    
    [SerializeField] private plantType _plantType;

    private float px;
    private float pz;
    private Vector3 raycastBeginPoint;

    private RaycastHit _hit;
    
    private List<WholePlantManager.Plant> plantsList;
    
    void Awake()
    {
        px = Random.Range(minRange, maxRange) * (Random.value<0.5 ? -1:1);
        pz = Random.Range(minRange, maxRange) * (Random.value<0.5 ? -1:1);
        raycastBeginPoint = transform.position + new Vector3(px, 15, pz);
        plantsList = WholePlantManager.instance.SendPlantsPosition();
    }

    private void Start()
    {
        Physics.Raycast(raycastBeginPoint, Vector3.down, out _hit, 30, Ground);
        if (_hit.collider != null)
        {
            transform.position = _hit.point;
            
            foreach (var plant in plantsList)
            {
                if (plant.plant.transform.position != this.transform.position)
                {
                    float distance = Vector3.Distance(this.transform.position, plant.plant.transform.position);
                    if (distance < cannotPLantDistance)
                    {
                        Destroy(this.gameObject);
                        return;
                    }
                }
            }
            
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            WholePlantManager.instance.AddAPlant(_plantType.ToString(),this.gameObject);
            Destroy(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
