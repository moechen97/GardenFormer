using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BudWiggle : MonoBehaviour
{
    [SerializeField] private float WiggleSpeed;
    [SerializeField] private float LifeLength;
    [SerializeField] private float appearSpeed;
    [SerializeField] private GameObject dissapearParticle;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Color finalColor;
    void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine("Appear");
        mesh.GetComponent<MeshRenderer>().material.DOColor(finalColor,LifeLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Appear()
    {
        transform.DOScale(Vector3.one, appearSpeed);
        transform.DOMove(transform.position + Vector3.up * 0.05f, WiggleSpeed).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(LifeLength);
        transform.DOScale(Vector3.one * 0.2f, appearSpeed);
        yield return new WaitForSeconds(appearSpeed/5);
        Instantiate(dissapearParticle, transform.position, quaternion.identity);
        Destroy(this.gameObject);
    }
}
