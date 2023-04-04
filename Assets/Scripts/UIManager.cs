using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private WeaponController weaponController;
    
    [SerializeField] 
    private GameObject equipmentWeaponUI;

    private void Update()
    {
        equipmentWeaponUI.SetActive(weaponController.IsFindWeapon);
    }
}
