using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using VertexAnimationTools_30;
using Random = UnityEngine.Random;

public class GrassGrow : MonoBehaviour
{
    [SerializeField] private float grassGrowSpeed;
    [SerializeField] private float grassOpenSpeed;
    [SerializeField] private float minThickness;
    [SerializeField] private float maxThickness;
    [SerializeField] private float unhealthSpeed;
    [SerializeField] private float unhealthNumber;
    [SerializeField] private MeshSequencePlayer _meshSequencePlayer;
    [SerializeField] private Transform topP;
    [SerializeField] private GameObject bud;
    [SerializeField] private float BloomGap;
    [SerializeField] private float hybridizeDistance;
    [SerializeField] private GameObject hybridizePlant;
     

    private Material grassMaterial;

    private float randomAdjust = 0;

    private bool cangrow = true;
    private bool fullyGrow = false;
    private float baselength=0;
    private float openextent=0;
    private float growLerpExtent = 0;
    private float openLerpExtent = 0;
    private float unhealthyLerpExtent = 0;
    
    private bool unhealthy = false;
    private float unhealthExtent = 0;

    private bool isBloom = false;
    public bool isHybrize = false;
    
    private List<WholePlantManager.Plant> plantsList;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        int state = transform.parent.GetChild(0).GetComponent<CheckDistance>().getPlantState();
        if (state == 1)
        {
            unhealthy = true;
            _meshSequencePlayer.NormalizedTime = 0.5f;
        }
        transform.localScale = Vector3.zero;
        randomAdjust = Random.Range(0.8f,1.3f);
        openextent = minThickness;
        grassMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        grassMaterial.SetFloat("_thickness",minThickness);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cangrow)
        {
            return;
        }

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

        if (unhealthy && unhealthyLerpExtent<1)
        {
            unhealthExtent = Mathf.Lerp(0, unhealthNumber, unhealthyLerpExtent);
            unhealthyLerpExtent += unhealthSpeed * Time.deltaTime;
            
            grassMaterial.SetFloat("_witheredExtent",unhealthExtent);
        }

        if (fullyGrow && !unhealthy && !isBloom)
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
                    
                    Instantiate(hybridizePlant,transform.position,quaternion.identity);
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

    public void IsUnhealthy()
    {
        unhealthy = true;
        _meshSequencePlayer.NormalizedTime = 0.5f;
    }
}
