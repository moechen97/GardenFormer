using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridPointGenerator : MonoBehaviour
{
    [SerializeField] private int totalpoints;

    [SerializeField] private GameObject pointSphere;
    [SerializeField] private Color UnocuppiedColor;
    [SerializeField] private Color OccupiedColor;
    [SerializeField] private float pointDistance;

    private static Dictionary<pointType, Color> pointBases = new Dictionary<pointType, Color>();
    
    private float x, y, z;

    private class GridPoints
    {
        public Vector3 pointPosition;
    }

    private List<GridPoints> gridpoints = new List<GridPoints>();

    public enum pointType
    {
        Empty,
        Occupied
    }
    
    void Start()
    {
        pointBases.Add(pointType.Empty,UnocuppiedColor);
        pointBases.Add(pointType.Occupied,OccupiedColor);
        
        //set up grid
        sunflowerDistrribute(totalpoints,pointDistance,transform.position.y);
        for (int i = 0; i < gridpoints.Count; i++)
        {
            Instantiate(pointSphere, gridpoints[i].pointPosition, quaternion.identity,this.transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sunflowerDistrribute(int totalnumber, float radius,float yposition)
    {
        
        for (int i = 1; i < totalnumber; i++)
        {
            float dist = Mathf.Pow(i / (totalnumber - 1f), 0.5f);
            float angle = 2 * Mathf.PI * 0.618033f * i;

            x = dist * Mathf.Cos(angle);
            z = dist * Mathf.Sin(angle);
            y = yposition;
            
            var newpoint = new GridPoints{pointPosition = new Vector3(x*radius,y,z*radius)};
            gridpoints.Add(newpoint);
        }

    }

   
}
