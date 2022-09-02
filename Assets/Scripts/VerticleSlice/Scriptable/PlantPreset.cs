using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Plant Preset",menuName = "Scriptables/Plant Preset",order = 2)]
public class PlantPreset : ScriptableObject
{
    public GameObject Flower;
    public float GrowUpSpeed;
    public float Florescence;
    public float Length;
    public float InitialHeight;
    public float FlowerGapTime;
    public float RandomHeightGap;

}
