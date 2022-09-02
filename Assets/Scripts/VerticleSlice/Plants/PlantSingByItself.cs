using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSingByItself : MonoBehaviour
{
    [SerializeField] private int GrassInterationNumber;
    [SerializeField] private GameObject EmitParticle;
    [SerializeField] private Transform top;

    public void Sing()
    {
        SoundManager.PlayGrassSound(GrassInterationNumber);
        Instantiate(EmitParticle, top.position, Quaternion.identity, top);
    }
}
