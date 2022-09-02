using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float enterPlantModeRoationSpeed;
    private bool isColddowning = false;
    private float horizontalRotationBias = -45;

    public void EnterPlantMode()
    {
        transform.DORotate(new Vector3(89, 0, 0), enterPlantModeRoationSpeed,RotateMode.Fast);
        isColddowning = true;
        StartCoroutine(EnterPlantModeColdDown());
    }
    
    public void ExitPlantMode()
    {
        transform.DORotate(new Vector3(45, 0, 0), enterPlantModeRoationSpeed,RotateMode.Fast);
        isColddowning = true;
        StartCoroutine(EnterPlantModeColdDown());
    }

    public void VerticalRotation()
    {
        if(isColddowning)
            return;
        float xRotation = transform.rotation.eulerAngles.x;
        float yRotation = transform.rotation.eulerAngles.y;
        
        Debug.Log("xRotation"+xRotation);
        
        
        horizontalRotationBias = horizontalRotationBias * -1 ;
        
        
        Debug.Log(xRotation+horizontalRotationBias);
        transform.DORotate(new Vector3(xRotation + horizontalRotationBias, yRotation, 0), rotationSpeed,RotateMode.Fast);

        isColddowning = true;
        StartCoroutine(ColdDown());
    }

    public void HorizontalRotation()
    {
        if(isColddowning)
            return;
        transform.DORotate(new Vector3(0, 90, 0), rotationSpeed,RotateMode.WorldAxisAdd);
        
        isColddowning = true;
        StartCoroutine(ColdDown());
    }

    IEnumerator ColdDown()
    {
        yield return new WaitForSeconds(rotationSpeed);
        isColddowning = false;
    }
    
    IEnumerator EnterPlantModeColdDown()
    {
        yield return new WaitForSeconds(enterPlantModeRoationSpeed);
        isColddowning = false;
    }
}
