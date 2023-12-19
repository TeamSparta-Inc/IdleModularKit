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
    [SerializeField] Button enhanceBtn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnClickSelectEquipment += SelectEquipment;

        equipBtn.onClick.AddListener(OnClickEquip);
        unEquipBtn.onClick.AddListener(OnClickUnEquip);
        enhanceBtn.onClick.AddListener(OnClickEnhance);
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
                SetOnEquippedBtnUI(selectEquipment.OnEquipped);
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

    void SetOnEquippedBtnUI(bool Onequipped)
    {
        if (Onequipped)
        {
            equipBtn.gameObject.SetActive(false);
            unEquipBtn.gameObject.SetActive(true);
        }
        else
        {
        equipBtn.gameObject.SetActive(true);
        unEquipBtn.gameObject.SetActive(false);
        }
    }


    

    public void OnClickEnhance()
    {
        selectEquipment.Enhance();
        SetselectEquipmentTextUI(selectEquipment);

        UpdateSelectEquipmentData();
    }

    public void OnClickEquip()
    {
        Debug.Log("장착 됨 ");
        selectEquipment.OnEquipped = true;
        SetOnEquippedBtnUI(selectEquipment.OnEquipped);
        Player.OnEquip?.Invoke(selectEquipment);
        UpdateSelectEquipmentData();
    }

    public void OnClickUnEquip()
    {
        selectEquipment.OnEquipped = false;
        SetOnEquippedBtnUI(selectEquipment.OnEquipped);
        Player.OnUnEquip?.Invoke(selectEquipment.type);
        UpdateSelectEquipmentData();
    }

    public void UpdateSelectEquipmentData()
    {
        EquipmentManager.SetEquipment(selectEquipment.name, selectEquipment);
    }
}
