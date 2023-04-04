using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponController : MonoBehaviour
{
    [Header("武器探す")]
    [SerializeField] 
    private Transform findWeaponRayStart;
    
    [SerializeField] 
    private Transform findWeaponRayEnd;

    [SerializeField] 
    private LayerMask weaponLayer;

    [Header("武器装備")]
    [SerializeField]
    private Transform weaponParent;

    [SerializeField] 
    private RigBuilder rigBuilder;

    [SerializeField] 
    private TwoBoneIKConstraint leftHandIK;
    
    [SerializeField] 
    private TwoBoneIKConstraint rightHandIK;

    private Weapon weapon = null;
    private bool isFindWeapon = false;
    public bool IsFindWeapon => isFindWeapon;

    private void Start()
    {
        rigBuilder.enabled = false;
    }

    private void Update()
    {
        Weapon foundWeapon = FindWeapon();
        isFindWeapon = foundWeapon != null;
        if (isFindWeapon && Input.GetKeyDown(KeyCode.E))
        {
            EquipWeapon(foundWeapon);
        }
    }
    
    private Weapon FindWeapon()
    {
        Vector3 rayVector = findWeaponRayEnd.position - findWeaponRayStart.position;
        Physics.Raycast(findWeaponRayEnd.position, rayVector, out RaycastHit hit, weaponLayer.value);
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<Weapon>();
        }

        return null;
    }

    private void EquipWeapon(Weapon equipWeapon)
    {
        weapon = equipWeapon;
        weapon.Equip(true);
        
        // 武器の位置、回転のみリセット
        Transform weaponTransform = weapon.transform;
        weaponTransform.SetParent(weaponParent);
        weaponTransform.localPosition = Vector3.zero;
        weaponTransform.localRotation = quaternion.identity;

        leftHandIK.data.target = weapon.LeftHandGrabPoint;
        rightHandIK.data.target = weapon.RightHandGrabPoint;
        
        rigBuilder.enabled = true;
    }
}
