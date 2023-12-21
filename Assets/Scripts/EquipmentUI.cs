using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Keiwando.BigInteger;

public class EquipmentUI : MonoBehaviour
{
    public static event Action<Equipment> OnClickSelectEquipment;
    public static Action<bool> UpdateEquipmentUI;

    public static EquipmentUI instance;

    [SerializeField] Equipment selectEquipment;
    [SerializeField] TMP_Text selectEquipmentName;
    [SerializeField] TMP_Text selectEquipment_equippedEffect;
    [SerializeField] TMP_Text selectEquipment_ownedEffect;
    [SerializeField] TMP_Text selectEquipment_enhancementLevel;

    [SerializeField] Button equipBtn;
    [SerializeField] Button unEquipBtn;
    [SerializeField] Button enhancePnaelBtn;
    [SerializeField] Button compositeBtn;


    [Header("강화 패널")]
    [SerializeField] Equipment enhanceEquipment; // 강화 무기
    [SerializeField] Button enhanceBtn; // 강화 버튼
    [SerializeField] TMP_Text enhanceLevelText; // 강화 레벨 / 장비 강화 (0/0)
    [SerializeField] TMP_Text EquippedPreview; // 장착 효과 미리보기 / 장착 효과 0 → 0
    [SerializeField] TMP_Text OwnedPreview;// 보유 효과 미리보기 / 보유 효과 0 → 0
    [SerializeField] TMP_Text EnhanceCurrencyText; // 현재 재화
    [SerializeField] TMP_Text RequiredCurrencyText; // 필요 재화


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnClickSelectEquipment += SelectEquipment;
        UpdateEquipmentUI += SetOnEquippedBtnUI;

        equipBtn.onClick.AddListener(OnClickEquip);
        unEquipBtn.onClick.AddListener(OnClickUnEquip);
        enhancePnaelBtn.onClick.AddListener(OnClickEnhancePanel);
        enhanceBtn.onClick.AddListener(OnClickEnhance);
        compositeBtn.onClick.AddListener(OnclickComposite);
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
        selectEquipment_equippedEffect.text = $"{BigInteger.ChangeMoney(equipment.equippedEffect.ToString())}%";
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


    public void OnClickEnhancePanel()
    {
        switch (selectEquipment.type)
        {
            case EquipmentType.Weapon:
                Equipment enhanceEquipmentTemp = EquipmentManager.GetEquipment(selectEquipment.name);

                Debug.Log("가보자" + enhanceEquipmentTemp.GetComponent<WeaponInfo>().myColor);

                enhanceLevelText.text = $"장비 강화 ({enhanceEquipmentTemp.enhancementLevel} / {enhanceEquipmentTemp.enhancementMaxLevel}</color>)"; //장비 강화(0 / 0)
                EquippedPreview.text = $"장착 효과 {enhanceEquipmentTemp.equippedEffect} → <color=green>{enhanceEquipmentTemp.equippedEffect + enhanceEquipmentTemp.basicEquippedEffect}</color>"; // 장착 효과 0 → 0
                OwnedPreview.text = $"보유 효과 {enhanceEquipmentTemp.ownedEffect} → <color=green>{enhanceEquipmentTemp.ownedEffect + enhanceEquipmentTemp.basicOwnedEffect}</color>";

                EnhanceCurrencyText.text = CurrencyManager.instance.GetCurrencyAmount("EnhanceStone");

                RequiredCurrencyText.text = enhanceEquipmentTemp.GetEnhanceStone().ToString();

                enhanceEquipment.GetComponent<WeaponInfo>().SetWeaponInfo(enhanceEquipmentTemp.GetComponent<WeaponInfo>());

                enhanceEquipment.SetUI();
                break;
        }

    }

    public void OnclickComposite()
    {
        EquipmentManager.instance.Composite(selectEquipment);

        selectEquipment.SetQuantityUI();

        UpdateSelectEquipmentData();
    }

    public void OnClickEnhance()
    {
        if (selectEquipment.enhancementLevel >= selectEquipment.enhancementMaxLevel) return;
        CurrencyManager.instance.SubtractCurrency("EnhanceStone",selectEquipment.GetEnhanceStone());
        selectEquipment.Enhance();
        SetselectEquipmentTextUI(selectEquipment);


        if (selectEquipment.OnEquipped) OnClickEquip();

        UpdateSelectEquipmentData();

        OnClickEnhancePanel();
    }

    public void OnClickEquip()
    {
        Debug.Log("장착 됨 ");
        Player.OnEquip?.Invoke(EquipmentManager.GetEquipment(selectEquipment.name));
        
    }

    public void OnClickUnEquip()
    {
        Player.OnUnEquip?.Invoke(selectEquipment.type);
        
    }

    public void UpdateSelectEquipmentData()
    {
        EquipmentManager.SetEquipment(selectEquipment.name, selectEquipment);
    }
}
