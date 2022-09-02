using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColorAndSize : MonoBehaviour
{
    [SerializeField] private float sizeMin;
    [SerializeField] private float sizeMax;
    [SerializeField] private Gradient meshColor;
    Renderer _meshRenderer;
    private float colorRandom;
    private float sizeRandom;
    
    
    void Awake()
    {
        _meshRenderer = transform.GetChild(0).GetComponent<Renderer>();
        colorRandom = Random.Range(0f,1f);
        sizeRandom = Random.Range(sizeMin, sizeMax);
        transform.localScale = Vector3.zero;
        _meshRenderer.material.color = meshColor.Evaluate(colorRandom); 
    }

    private void Start()
    {
        transform.DOScale(Vector3.one * sizeRandom, 3f);
    }
    
}
