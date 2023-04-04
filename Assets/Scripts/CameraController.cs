using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラ追従
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform moveObject;

    //[SerializeField] 
    //private Rigidbody moveRotateObjectRigidbody;
    
    [SerializeField]
    private Transform moveTarget;

    //[SerializeField] 
    //private float moveForce;  // カメラをゆっくり目標位置を近付きたいが失敗したので、一旦コメントアウト

    [SerializeField] 
    private Transform lookAt;

    [SerializeField] 
    private Transform cameraTransform;

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        // カメラ追従
        moveObject.position = moveTarget.position;
        // カメラ位置
        cameraTransform.position = moveObject.position;
        // カメラ向き
        cameraTransform.LookAt(lookAt);
    }
}
