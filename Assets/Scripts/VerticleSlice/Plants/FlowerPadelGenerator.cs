using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlowerPadelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject petal;
    [SerializeField] private float petalNumber;
   

    private void Start()
    {
        for (int i = 0; i < petalNumber; i++)
        {
            float rotationX = 360 / petalNumber;
            Vector3 rotationAngle = transform.rotation.eulerAngles + new Vector3(0, 0, rotationX * i);
            Instantiate(petal, transform.position, Quaternion.Euler(rotationAngle), this.transform);
            
        }
    }

    public void FlowerDisappear()
    {
        transform.DOScale(Vector3.zero, 1.5f);
        StartCoroutine(destoryFlower());
    }

    IEnumerator destoryFlower()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
