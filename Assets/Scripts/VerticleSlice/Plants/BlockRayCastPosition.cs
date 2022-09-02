using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRayCastPosition : MonoBehaviour
{
   [SerializeField] private float distance;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private Vector3 raycastOffset;
   private RaycastHit _hit;
   private Transform block;
   private Vector3 raycastOrigin;

   private void Awake()
   {
      
   }

   private void Update()
   {
   }

   private void FixedUpdate()
   {
      raycastOrigin = transform.position + raycastOffset;
      if (Physics.Raycast(raycastOrigin, -transform.up, out _hit, distance, groundLayer))
      {
         Debug.Log(_hit.transform.name);
         transform.position = _hit.point; 
      }
      
   }
}
