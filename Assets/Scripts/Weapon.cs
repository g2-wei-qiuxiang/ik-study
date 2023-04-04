using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] 
    private Transform leftHandGrabPoint;
    public Transform LeftHandGrabPoint => leftHandGrabPoint;
    
    [SerializeField] 
    private Transform rightHandGrabPoint;
    public Transform RightHandGrabPoint => rightHandGrabPoint;

    [SerializeField] 
    private Collider[] colliders;

    [SerializeField] 
    private Rigidbody rigidbody;

    public void Equip(bool isOn)
    {
        foreach (var colliderComponent in colliders)
        {
            colliderComponent.enabled = !isOn;
        }
        rigidbody.isKinematic = isOn;
    }
}
