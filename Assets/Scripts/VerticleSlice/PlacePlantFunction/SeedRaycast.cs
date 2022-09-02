using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeedRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayermask;
    [SerializeField] private float positionOffset;
    
    public enum plantType
    {
        SphereFlower,
        BoxFlower,
        CylinderFlower,
        Grass
    };
    
    [SerializeField] private plantType _plantType;
 
    private Touch fingerTouch;
    private Button quitButton;
    private PlantButton _PlantButton;
    
    private AudioSource _soundEffectPlayer;
    
    private string ButtonName;
    
    private Transform MainCanvas;
    
    private bool isPlaced=false;
    private bool isCancled = false;
    
    private BlockRayCastPosition _blockRayCast;

    private RaycastHit hit;
    
    private InstantiateBlock _instantiateBlock;
    
    private Transform cameraTransform;
    
    
    void Awake()
    {
        cameraTransform = FindObjectOfType<UsingFingerRotatePlane>().transform;
        
        _blockRayCast = GetComponent<BlockRayCastPosition>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        MainCanvas = GameObject.Find("MainCanvas").transform;
        _PlantButton = GameObject.Find("PlantButton").GetComponent<PlantButton>();
        
        
    }

    
    void Update()
    {
        if (isPlaced)
        {
            return;
        }
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            cameraTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        if (Input.touchCount == 1)
        {
            
            fingerTouch = Input.GetTouch(0);
            
            if (fingerTouch.phase == TouchPhase.Moved)
            {
                var ray = Camera.main.ScreenPointToRay(fingerTouch.position);
                
                Physics.Raycast(ray.origin, ray.direction, out hit, 10f, groundLayermask);
                if (hit.collider != null)
                {
                    transform.position = hit.point+Vector3.forward*positionOffset;
                }
                
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
    
    
    
    
}
