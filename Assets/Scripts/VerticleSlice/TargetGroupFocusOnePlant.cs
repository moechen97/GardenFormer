using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TargetGroupFocusOnePlant : MonoBehaviour
{
    [SerializeField] private LayerMask plantLayer;
    
    private CinemachineTargetGroup _targetGroup;
    private bool canFocusOnOne = true;
    private Touch _touch;
    private RaycastHit hit;
    private RaycastHit hit2;
    
    void Awake()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();
    }
    
    void Update()
    {
        if (!canFocusOnOne)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                var ray = Camera.main.ScreenPointToRay(_touch.position);
                
                Physics.Raycast(ray.origin, ray.direction, out hit, 10f, plantLayer);
                Physics.Raycast(ray.origin, ray.direction, out hit2, 10f);

                if (hit2.collider == null)
                {
                    if (_targetGroup.m_Targets.Length > 1)
                    {
                        Transform removeTransform = _targetGroup.m_Targets[1].target.transform;
                        _targetGroup.RemoveMember(removeTransform);
                    }
                }
                if(hit.collider != null)
                {
                    if (_targetGroup.m_Targets.Length == 1)
                    {
                        _targetGroup.AddMember(hit.collider.transform,15f,0f);
                        
                    }
                }
            }
            
           
        }

    }

    public void EnterPlantMode()
    {
        canFocusOnOne = !canFocusOnOne;
        if (_targetGroup.m_Targets.Length > 1)
        {
            Transform removeTransform = _targetGroup.m_Targets[1].target.transform;
            _targetGroup.RemoveMember(removeTransform);
        }
    }
}
