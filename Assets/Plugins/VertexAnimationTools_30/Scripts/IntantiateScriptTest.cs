using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntantiateScriptTest : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    [SerializeField] private int num;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(cube);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
