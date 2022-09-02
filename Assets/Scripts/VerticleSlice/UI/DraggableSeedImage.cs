using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DraggableSeedImage : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private GameObject Plant;

    private Touch _touch;
    private string gbuttomName;

    private RaycastHit hit;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                transform.position = _touch.position;
                
                var ray = Camera.main.ScreenPointToRay(_touch.position);
                
                Physics.Raycast(ray.origin, ray.direction, out hit, 10f, groundLayerMask);

                if (hit.collider != null)
                {
                    GameObject generatedPlant = Instantiate(Plant, hit.point, quaternion.identity);
                    generatedPlant.GetComponent<Seed_Ver_2>().GeneratedButtonRecord(gbuttomName);
                    Destroy(this.gameObject);
                }
                
            }
            
            if ((_touch.phase == TouchPhase.Ended) || (_touch.phase == TouchPhase.Canceled))
            {
                GameObject.Find(gbuttomName).GetComponent<InstantiateBlock>().canclePlantBlock();
                Destroy(this.gameObject);
            }
        }
    }

    public void GetGenerateButtonName(string buttomName)
    {
        gbuttomName = buttomName;
    }
}
