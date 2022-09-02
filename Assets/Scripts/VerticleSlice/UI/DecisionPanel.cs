using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionPanel : MonoBehaviour
{
    private Seed_Ver_2 _seed;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Image plantImage;
    private bool isEntered = false;
    
    public void GetSeed(Seed_Ver_2 generatorSeed)
    {
        _seed = generatorSeed;
    }
        
    public void DonnotPlantHere()
    {
        _seed.ClosePanel();
    }

    public void FingerEnterImage()
    {
        Debug.Log("Decide Plant Seed!");
        if (!isEntered)
        {
            plantImage.color = selectedColor;
            StartCoroutine(PlantSeed());
            isEntered = true;
        }
    }

    public void FingerExitImage()
    {
        Debug.Log("Not Plant Seed!");
        //_seed.ExitDecide();
    }

    IEnumerator PlantSeed()
    {
        yield return new WaitForSeconds(0.3f);
        _seed.EnterDecide();
    }
}
