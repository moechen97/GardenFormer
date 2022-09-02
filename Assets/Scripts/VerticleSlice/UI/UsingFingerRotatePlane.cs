using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;

public class UsingFingerRotatePlane : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float withoutGyroSpeed;
    [SerializeField] private float plantModeSpeed;
    [SerializeField] private float xRotateMax;
    [SerializeField] private float xRotateMin;
    [SerializeField] private float gyroYModify;
    [SerializeField] private float gyroXModify;
    [SerializeField] private LayerMask plotLayerMask;
    

    private Touch _touch;
    private bool canRotateCamera = true;

    private bool usingGyro = true;
    private bool supportGyro = false;
    
    private float yRotation;
    private float xrotation;

    private Gyroscope gyro;
    
    
    private Vector3 accelerometerModify;
    private Quaternion rot;

    private bool canUseGyro = true;

    private RaycastHit hit = default;
    
  

    private void Awake()
    {
        supportGyro = EnableGyro();
        if (!supportGyro)
        {
            return;
        }
       
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            
            transform.rotation = Quaternion.Euler(0,0,0);
            rot = new Quaternion(0, 0, 1, 0)*Quaternion.Euler(90,0,0);
            
            return true;
        }

        return false;
    }

    void Update()
    {

        if (usingGyro && supportGyro && canUseGyro)
        {
            gyro = Input.gyro;
            
            xrotation = -gyro.rotationRateUnbiased.x*gyroXModify;
            yRotation = -gyro.rotationRateUnbiased.y*gyroYModify;

        }
        
        if (Input.touchCount == 1)
        {

            _touch = Input.GetTouch(0);

            if (!canRotateCamera)
            {
                var ray = Camera.main.ScreenPointToRay(_touch.position);
                
                Physics.Raycast(ray.origin, ray.direction, out hit, 10f, plotLayerMask);
                if (hit.collider == null)
                {
                    return;
                }
            }
           

            if (_touch.phase == TouchPhase.Moved)
            {
                float rotateSpeed = Speed;
                if (!usingGyro && canRotateCamera)
                {
                    rotateSpeed = withoutGyroSpeed;
                }
                else if (!usingGyro && !canRotateCamera)
                {
                    rotateSpeed = plantModeSpeed;
                }
                
                yRotation += _touch.deltaPosition.x * rotateSpeed*Time.deltaTime;

                if (canRotateCamera)
                {
                    xrotation += -_touch.deltaPosition.y * rotateSpeed*Time.deltaTime;
                }
                else
                {
                    xrotation = 0;
                }
                    
                
                //Debug.Log(xrotation);
            }
           
        }
        else if (Input.touchCount == 0)
        {
            yRotation += 0;

            xrotation += 0;

            if (!usingGyro || !canRotateCamera)
            {
                yRotation = 0;
                xrotation = 0;
            }
        }

        
        
        float xAngle = this.transform.rotation.eulerAngles.x;

        if (xAngle + xrotation < xRotateMax&&xAngle+xrotation>xRotateMin)
        {
            //xrotation = xrotation;
        }
        else if (xAngle + xrotation > xRotateMax || xAngle+xrotation<xRotateMin )
        {
            xrotation = 0;
        }
        
        

        this.transform.rotation =
            Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(xrotation, yRotation, 0));
    }

    public void SwitchGyroState()
    {
        usingGyro = !usingGyro;
    }

    public void DisableFingerRotate()
    {
        canRotateCamera = false;
        canUseGyro = false;
        xrotation = 0;
        yRotation = 0;
    }

    public void EnableFingerRotate()
    {
        canRotateCamera = true;
        canUseGyro = true;
    }
} 
