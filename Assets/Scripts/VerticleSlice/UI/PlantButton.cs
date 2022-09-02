using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{
    [SerializeField] private Color unInteratableColor;
    [SerializeField] private Color InteractableColor;
    
    

    private Image buttonImage;
    private Button _button;
    
    private Seed_Ver_2 beingPlantedSeed;
    
    public static PlantButton  instance { get; private set; }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);  
        }
        
        buttonImage = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.interactable = false;
    }

    public void PlantTheSeed()
    {
        beingPlantedSeed.PlaceObject();
    }

    public void GetSeed(Seed_Ver_2 seed)
    {
        beingPlantedSeed = seed;
    }

    public void SetButtonUnInteractable()
    {
        _button.interactable = false;
        buttonImage.color = unInteratableColor;
    }
    
    public void SetButtonInteractable()
    {
        _button.interactable = true;
        buttonImage.color = InteractableColor;
    }
}
