using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PetalGrow : MonoBehaviour
{
    [SerializeField] private float growSpeed;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        transform.DOScale(Vector3.one, growSpeed);
    }

    
}
