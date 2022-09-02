using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotationRamdomSize : MonoBehaviour
{
    private float randomadjust;
    
    void Start()
    {
        randomadjust = Random.Range(0.8f, 1.2f);
        transform.localScale = Vector3.one*randomadjust;

        float randomRotationy = Random.Range(0f, 360f);
        
        transform.rotation = quaternion.Euler(new Vector3(0,randomRotationy,0));

    }
    
}
