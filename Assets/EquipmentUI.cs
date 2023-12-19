using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public static event Action<Equipment> OnClickSelectEquipment;

    public static EquipmentUI instance;

    [SerializeField] Equipment selectEquipment;
    [SerializeField] TMP_Text selectEquipmentName;
    [SerializeField] TMP_Text selectEquipment_equippedEffect;
    [SerializeField] TMP_Text selectEquipment_ownedEffect;
    [SerializeField] TMP_Text selectEquipment_enhancementLevel;

    [SerializeField] Button equipBtn;
    [SerializeField] Button unEquipBtn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnClickSelectEquipment += SelectEquipment;

        equipBtn.onClick.AddListener(OnEquip);
        //unEquipBtn.onClick.AddListener(OnUnEquip);
    }



    public static void TriggerSelectEquipment(Equipment equipment)
    {
        OnClickSelectEquipment?.Invoke(equipment);
    }



    public void SelectEquipment(Equipment equipment)
    {
        switch (selectEquipment.type)
        {
            case EquipmentType.Weapon:
                equipment.SetQuantityUI();
                selectEquipment.GetComponent<WeaponInfo>().SetWeaponInfo(equipment.GetComponent<WeaponInfo>());
                selectEquipment.GetComponent<WeaponInfo>().SetUI();
                break;
        }
        SetselectEquipmentTextUI(equipment);
    }


    void SetselectEquipmentTextUI(Equipment equipment)
    {
        selectEquipmentName.text = equipment.name;
        selectEquipment_equippedEffect.text = $"{equipment.equippedEffect}%";
        selectEquipment_ownedEffect.text = $"{equipment.ownedEffect}%";
    }


    public void OnEquip()
    {
        Debug.Log("장착 됨 ");
        Player.OnEquip?.Invoke(selectEquipment);
    }
    public void OnUnEquip()
    {
        Player.OnUnEquip?.Invoke(selectEquipment.type); 
    }
}
