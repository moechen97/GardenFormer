using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float zMin;
    [SerializeField] private float zMax;
    private Vector2 fingerPosition;
    private Touch fingerTouch;
    private Transform cameraTransform;
    private bool isPlaced=false;
    private float moveX;
    private float moveZ;
    private Button placeButton;
    private string ButtonName;
    private InstantiateBlock _instantiateBlock;
    
    private BlockRayCastPosition _blockRayCast;

    private void Awake()
    {
        cameraTransform = FindObjectOfType<UsingFingerRotatePlane>().transform;
        placeButton = GameObject.Find("PlaceButton").GetComponent<Button>();
        _blockRayCast = GetComponent<BlockRayCastPosition>();

    }

    void Update()
    {
        if (isPlaced)
        {
            return;
        }
        
        /*transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            cameraTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);*/
        
        if (Input.touchCount > 0)
        {
            
            fingerTouch = Input.GetTouch(0);
            if (fingerTouch.phase == TouchPhase.Moved)
            {
                moveX = fingerTouch.deltaPosition.x * moveSpeed * Time.deltaTime;
                moveZ = fingerTouch.deltaPosition.y * moveSpeed * Time.deltaTime;
                transform.position += transform.forward*moveZ + transform.right*moveX;
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y,
                    Mathf.Clamp(transform.position.z, zMin, zMax));
            }
        }
        
        placeButton.onClick.AddListener(PlaceObject);

        if (EventSystem.current.currentSelectedGameObject!=null)
        {
            if (EventSystem.current.currentSelectedGameObject != placeButton.gameObject && 
                EventSystem.current.currentSelectedGameObject.name != ButtonName 
                && EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("Button"))
            {
                _instantiateBlock.resetCreateBlock();
                Destroy(this.gameObject);
            }
        }
        
    }
    

    void PlaceObject()
    {
        //Debug.Log("click place button");
        isPlaced = true;
        Destroy(_blockRayCast);
        GameObject generatedButton = GameObject.Find(ButtonName);
        Destroy(generatedButton);
    }

    public void GeneratedButtonRecord(string name)
    {
        ButtonName = name;
        _instantiateBlock = GameObject.Find(ButtonName).GetComponent<InstantiateBlock>();
    }
}
