using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrassSpread : MonoBehaviour
{
    [SerializeField] private GameObject grassblock;
    [SerializeField] private int maxIteration;
    [SerializeField] private int maxChildernNumber;
    [SerializeField] private int minChildernNumber;
    [SerializeField] private float SpreadSpeed;
    [SerializeField] private float generateRadius;
    [SerializeField] private LayerMask groundLayer;
    private RaycastHit _hit;

    private bool canSpread = false;
    private int generation = 0;

    private float timeCount = 0;
    private float timeRecord = 0;

    private bool isSpreaded = false;
    

    private void Update()
    {
        if(isSpreaded)
            return;
        
        if (canSpread)
        {
            timeCount += Time.deltaTime;
            
            if (timeCount > SpreadSpeed)
            {
                if(generation<maxIteration)
                {
                    
                    int childernNumber = Random.Range(minChildernNumber, maxChildernNumber);
        
                    for (int i = 0; i < childernNumber; i++)
                    {
                        float randomNumber = Random.Range(0.5f, 1f);
                        float randomx = Random.Range(-1f, 1f);
                        float randomy = Random.Range(-1f, 1f);
                        
                        
                        Vector3 randomGeneratePosition = new Vector3(transform.position.x + randomNumber * randomx * generateRadius,
                            5, transform.position.z + randomNumber * randomy * generateRadius);
                        if (Physics.Raycast(randomGeneratePosition, -transform.up, out _hit, 10, groundLayer))
                        {
                            var newgrass = Instantiate(grassblock,_hit.point, quaternion.identity,this.gameObject.transform);
                            
                        }
                    }


                    timeCount = 0;
                    generation += 1;
                }
                else
                {
                    isSpreaded = true;
                }
                
            }
        }
    }
    
    
    
    private void Awake()
    {
        WholePlantManager.PlantsCanGrow += CanSpread;
        WholePlantManager.PlantsCannotGrow += CannotSpread;
        
    }

    private void Start()
    {
        //timeRecord = Time.time;
        Debug.Log("beginning time " + Time.time);
    }

    private void OnDisable()
    {
        WholePlantManager.PlantsCanGrow -= CanSpread;
        WholePlantManager.PlantsCannotGrow -= CannotSpread;
    }

    private void OnDestroy()
    {
        WholePlantManager.PlantsCanGrow -= CanSpread;
        WholePlantManager.PlantsCannotGrow -= CannotSpread;
    }

    private void CannotSpread()
    {
        canSpread = false;
    }

    private void CanSpread()
    {
        Debug.Log("let grass grow");
        /*if (canSpread == false)
        {
            timeRecord = Time.time-timeCount + timeRecord;
            Debug.Log("start"+ "timeCount"+timeCount + "Time" + Time.time+ " timeRecord" + timeRecord);
            
        }*/
        canSpread = true;
        

    }
    
}
