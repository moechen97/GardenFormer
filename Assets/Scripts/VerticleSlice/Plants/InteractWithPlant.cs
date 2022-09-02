using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InteractWithPlant : MonoBehaviour
{
    [SerializeField] private LayerMask plantLayer;
    [SerializeField] private float interactiveColdown;
    [SerializeField] private GameObject InteractParticle;
    [SerializeField] private Transform top;
    [SerializeField] private GameObject EmitParticle;
    [SerializeField] private int GrassInterationNumber;
    
    private Touch _touch;
    private Ray _ray;
    private RaycastHit _hit;

    private bool canInteract = true;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (canInteract && _touch.phase==TouchPhase.Began)
            {
                var ray = Camera.main.ScreenPointToRay(_touch.position);
                
                Physics.Raycast(ray.origin, ray.direction, out _hit, 20f, plantLayer);
                if (_hit.collider == null)
                {
                    return;
                }
                
                if (_hit.collider.transform.position != transform.position)
                {
                    return;
                }

                SoundManager.PlayGrassSound(GrassInterationNumber);
                canInteract = false;
                Instantiate(InteractParticle, _hit.point, quaternion.identity);
                Instantiate(EmitParticle, top.position, quaternion.identity, top);
                StartCoroutine("interactColddowm");
            }
        }
    }

    IEnumerator interactColddowm()
    {
        yield return new WaitForSeconds(interactiveColdown);
        canInteract = true;
    }
}
