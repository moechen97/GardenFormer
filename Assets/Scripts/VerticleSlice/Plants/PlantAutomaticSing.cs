using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAutomaticSing : MonoBehaviour
{
    [SerializeField] private float noInteractionTime;
    [SerializeField] private float rhythmLag;
    private List<WholePlantManager.Plant> plantsList;
    private float countTime = 0;
    private float recordTime = 0;
    private float recordingTime = 0;
    private bool canAutomaticSing = false;
    private bool cansing = true;
    
    
    void Start()
    {
        recordingTime = Time.time;
    }

    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            recordTime = Time.time;
            canAutomaticSing = false;
        }

        if (!canAutomaticSing)
        {
            countTime = Time.time - recordTime;
            
            if (countTime > noInteractionTime)
            {
                canAutomaticSing = true;
            }
        }
        else
        {
            if (cansing)
            {
                plantsList = WholePlantManager.instance.SendPlantsPosition();

                if (plantsList!= null)
                {
                    int x = Random.Range(0, plantsList.Count-1);
                
                    plantsList[x].plant.GetComponent<PlantSingByItself>().Sing();

                    StartCoroutine("singcolddown");

                    cansing = false;
                }
                
            }
        }
       
        
        
    }

    private IEnumerator singcolddown()
    {
        yield return new WaitForSeconds(rhythmLag);
        cansing = true;
    }
}
