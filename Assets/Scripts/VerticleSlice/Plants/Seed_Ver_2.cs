using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Seed_Ver_2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float zMin;
    [SerializeField] private float zMax;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask plotMask;


    public enum plantType
    {
        SphereFlower,
        BoxFlower,
        CylinderFlower,
        Grass
    };

    [SerializeField] private plantType _plantType;
    
    
    private Vector2 fingerPosition;
    private Touch fingerTouch;
    private Transform cameraTransform;
    private bool isPlaced=false;
    private float moveX;
    private float moveZ;
    private Button placeButton;
    private string ButtonName;
    private InstantiateBlock _instantiateBlock;
    
    //private Transform PlantPanel;
    
    
    
    private float NewTimeCount = 0;
    private Vector2 initialPosition;
    private bool isFingerMove = false;
    private Button noButton;
    private Button yesButton;
    private Button canclePlantingButton;
    private Button quitButton;

    private bool isCancled = false;

    private bool secondFingerMoved = false;

    private GameObject decisionPanel;
    private bool decideToPlant = false;
    private bool canChangeDecisonState = true;

    private Transform MainCanvas;
    
    
    private BlockRayCastPosition _blockRayCast;

    private AudioSource _soundEffectPlayer;

    private PlantButton _PlantButton;

    private RaycastHit hit;
    private RaycastHit hitplot;

    private void Awake()
    {
        cameraTransform = FindObjectOfType<UsingFingerRotatePlane>().transform;
        //PlantPanel = GameObject.Find("PlantPanelController").transform.GetChild(0);
        _blockRayCast = GetComponent<BlockRayCastPosition>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        MainCanvas = GameObject.Find("MainCanvas").transform;
        _PlantButton = GameObject.Find("PlantButton").GetComponent<PlantButton>();
        StartCoroutine(setIndicatorActive());
        
        _PlantButton.GetSeed(this);
        _PlantButton.SetButtonInteractable();

    }

    void Update()
    {
        if (isPlaced)
        {
            return;
        }
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            cameraTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        if (Input.touchCount > 0 )
        {

            fingerTouch = Input.GetTouch(0);

            //two finger click cancle
            if (Input.touchCount > 1)
            {
                Touch secondfingerTouch = Input.GetTouch(1);

                if (secondfingerTouch.phase == TouchPhase.Moved)
                {
                    secondFingerMoved = true;
                }
                
                if (fingerTouch.phase == TouchPhase.Ended || secondfingerTouch.phase == TouchPhase.Ended)
                {
                    if (!secondFingerMoved)
                    {
                        CanclePlanting();
                    }

                    secondFingerMoved = false;
                }
            }
            
            else if (Input.touchCount == 1)
            {
                //Hold and Drag
                if (fingerTouch.phase == TouchPhase.Moved)
                {
                    Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out hit, 10f, groundMask);
                    
                    var ray = Camera.main.ScreenPointToRay(fingerTouch.position);
                
                    Physics.Raycast(ray.origin, ray.direction, out hitplot, 10f, plotMask);

                    if (hitplot.collider != null)
                    {
                        return;
                    }
                    
                    if (hit.collider != null)
                    {
                        isFingerMove = true;
                
                        moveX = fingerTouch.deltaPosition.x * moveSpeed * Time.deltaTime;
                        moveZ = fingerTouch.deltaPosition.y * moveSpeed * Time.deltaTime;
                        transform.position += transform.forward * moveZ + transform.right * moveX;
                        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y,
                            Mathf.Clamp(transform.position.z, zMin, zMax));   
                    }
                    else
                    {
                        Vector3 moveback = Vector3.zero-transform.position;
                        transform.position += moveback * 0.001f;
                    }
                }

                if (fingerTouch.phase == TouchPhase.Began)
                {
                    initialPosition = fingerTouch.position;
                   

                    decideToPlant = false;


                }

                

                if (fingerTouch.phase == TouchPhase.Ended)
                {
                    isFingerMove = false;
                    
                }
            }
            
            
            
        }
        
        if (EventSystem.current.currentSelectedGameObject!=null)
        {
          
            if (EventSystem.current.currentSelectedGameObject.name != ButtonName 
                && EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("Button"))
            {
                _instantiateBlock.canclePlantBlock();
                Destroy(this.gameObject);
            }
            
            
        }
        
        
        quitButton.onClick.AddListener(CanclePlanting);
        
        
    }

    private void CanclePlanting()
    {
        if (isPlaced||isCancled)
            return;
        
        isCancled = true;
        
        _PlantButton.SetButtonUnInteractable();
        
        if (noButton != null)
        {
            noButton.transform.parent.gameObject.SetActive(false);
        }
        _instantiateBlock.canclePlantBlock();
        Destroy(this.gameObject);
    }

    public void ClosePanel()
    {
        //noButton.transform.parent.gameObject.SetActive(false);
       
    }


    public void PlaceObject()
    {
        //Debug.Log("click place button");
        isPlaced = true;
        Destroy(_blockRayCast);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        if (_plantType != plantType.Grass)
        {
            WholePlantManager.instance.AddAPlant(_plantType.ToString(),this.gameObject);
        }
        //noButton.transform.parent.gameObject.SetActive(false);
        //isPanelShowUp = false;
        _instantiateBlock.resetCreateBlock();
       
        _PlantButton.SetButtonUnInteractable();
        
        SoundManager.PlaySound();
    }

    public void GeneratedButtonRecord(string name)
    {
        ButtonName = name;
        _instantiateBlock = GameObject.Find(ButtonName).GetComponent<InstantiateBlock>();
    }

    public void EnterDecide()
    {
        decideToPlant = true;
        PlaceObject();
        Destroy(decisionPanel,0.05f);
        
    }

    IEnumerator setIndicatorActive()
    {
        yield return new WaitForSeconds(0.05f);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    
}
