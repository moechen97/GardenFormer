using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrow_donotneedPlant : MonoBehaviour
{
    [SerializeField] private float grassGrowSpeed;
    [SerializeField] private float grassOpenSpeed;
    [SerializeField] private float minThickness;
    [SerializeField] private float maxThickness;
    [SerializeField] private Transform topP;
    [SerializeField] private GameObject bud;
    [SerializeField] private float BloomGap;
    [SerializeField] private bool canHybridize;
    [SerializeField] private float hybridizeDistance;
    [SerializeField] private GameObject BabyPlant;
    
    private Material grassMaterial;

    private float randomAdjust = 0;

    private bool cangrow = true;
    private bool fullyGrow = false;
    private float baselength=0;
    private float openextent=0;
    private float growLerpExtent = 0;
    private float openLerpExtent = 0;
    bool isHybrize = false;
    private List<WholePlantManager.Plant> plantsList;
    
    private bool isBloom = false;
    
    void Awake()
    {
        transform.localScale = Vector3.zero;
        randomAdjust = Random.Range(0.8f,1.3f);
        openextent = minThickness;
        grassMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        grassMaterial.SetFloat("_thickness",minThickness);
    }

    
    void Update()
    {
        if (!fullyGrow)
        {
            
            if(growLerpExtent<1)
            {
                baselength = Mathf.Lerp(0, randomAdjust, growLerpExtent);
                growLerpExtent += grassGrowSpeed * Time.deltaTime;
                
                transform.localScale = Vector3.one*baselength;
            }

            if (openLerpExtent<1)
            {
                openextent = Mathf.Lerp(minThickness, maxThickness, openLerpExtent);
                openLerpExtent += grassOpenSpeed * Time.deltaTime;
                
                grassMaterial.SetFloat("_thickness",openextent);
            }

            if (growLerpExtent >= 1 && openLerpExtent >= 1)
            {
                fullyGrow = true;
            }
            
        }
        
        if (fullyGrow && !isBloom)
        {
            Instantiate(bud,topP.position, Quaternion.identity, topP);
            isBloom = true;
            StartCoroutine("bloomColdown");
            checkHybridization();
        }
        
        
        
        
    }
    
    void checkHybridization()
    {
        if (isHybrize)
            return;
        
        plantsList = WholePlantManager.instance.SendPlantsPosition();
        foreach (var plant in plantsList)
        {
            if (plant.plant.transform.position != this.transform.position)
            {
                float distance = Vector3.Distance(this.transform.position, plant.plant.transform.position);
                if (distance < hybridizeDistance)
                {
                    
                    Instantiate(BabyPlant,transform.position,Quaternion.identity);
                    isHybrize = true;
                    return;
                }
            }
        }
    }
    
    private IEnumerator bloomColdown()
    {
        float x = Random.Range(BloomGap - 5, BloomGap + 5);
        yield return new WaitForSeconds(x);
        isBloom = false;
    }
    
}
