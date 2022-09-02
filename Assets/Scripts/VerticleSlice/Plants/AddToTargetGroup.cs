using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AddToTargetGroup : MonoBehaviour
{
    private CinemachineTargetGroup _targetGroup;


    private void Awake()
    {
        _targetGroup = FindObjectOfType<CinemachineTargetGroup>();
        _targetGroup.AddMember(this.gameObject.transform,0.3f,0);
    }

    private void OnDestroy()
    {
        _targetGroup.RemoveMember(this.gameObject.transform);
    }
}
