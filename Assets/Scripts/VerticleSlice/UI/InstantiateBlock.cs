using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateBlock : MonoBehaviour
{
    //[SerializeField] private GameObject block;
    [SerializeField] private GameObject SeedImage;
    [SerializeField] private Text countText;
    [SerializeField] private int number;
    Transform mainCanvas;


    private bool creatBlock = false;
    private int count;

    private void Awake()
    {
        count = number;
        countText.text = count.ToString();
        mainCanvas = GameObject.Find("MainCanvas").transform;

    }

    public void instantiateTheBlock()
    {
        if(creatBlock || count<=0)
            return;
        
        /*GameObject createdBlock = Instantiate(block, new Vector3(0, 4, 0), Quaternion.identity);
        createdBlock.GetComponent<Seed_Ver_2>().GeneratedButtonRecord(this.name);*/

        GameObject seedImage = Instantiate(SeedImage, Input.GetTouch(0).position, quaternion.identity,mainCanvas);
        seedImage.GetComponent<DraggableSeedImage>().GetGenerateButtonName(this.name);
        
        count -= 1;
        countText.text = count.ToString();

        creatBlock = true;

    }

    public void resetCreateBlock()
    {
        creatBlock = false;
    }

    public void canclePlantBlock()
    {
        creatBlock = false;
        count += 1;
        countText.text = count.ToString();
    }
}
