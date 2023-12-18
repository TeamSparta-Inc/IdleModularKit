using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EquipmentUI : MonoBehaviour
{
    public static event Action<Equipment> OnClickSelectEquipment;

    public static EquipmentUI instance;

    [SerializeField] Equipment selectEquipment;
    [SerializeField] TMP_Text selectEquipmentName;
    [SerializeField] TMP_Text selectEquipment_equippedEffect;
    [SerializeField] TMP_Text selectEquipment_ownedEffect;
    [SerializeField] TMP_Text selectEquipment_enhancementLevel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnClickSelectEquipment += SelectEquipment;
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
                selectEquipment.GetComponent<WeaponInfo>().SetWeaponInfo(equipment.GetComponent<WeaponInfo>());
                selectEquipment.GetComponent<WeaponInfo>().SetUI();
                break;
        }
        SetselectEquipmentTextUI(equipment);
    }

    void SetselectEquipmentTextUI(Equipment equipment)
    {
        selectEquipmentName.text = equipment.name;
        selectEquipment_equippedEffect.text = equipment.equippedEffect;
        selectEquipment_ownedEffect.text = equipment.ownedEffect;
    }
}
