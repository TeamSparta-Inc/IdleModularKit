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
        //SetupEventListeners();
        InitializeButtonListeners();

    }

    // 이벤트 설정하는 메서드
    private void OnEnable()
    {
        OnClickSelectEquipment += SelectEquipment;
        UpdateEquipmentUI += SetOnEquippedBtnUI;
    }

    private void OnDisable()
    {
        OnClickSelectEquipment -= SelectEquipment;
        UpdateEquipmentUI -= SetOnEquippedBtnUI;
    }

    // 버튼 클릭 리스너 설정하는 메서드 
    void InitializeButtonListeners()
    {
        equipBtn.onClick.AddListener(OnClickEquip);
        unEquipBtn.onClick.AddListener(OnClickUnEquip);
        enhancePnaelBtn.onClick.AddListener(OnClickEnhancePanel);
        enhanceBtn.onClick.AddListener(OnClickEnhance);
        compositeBtn.onClick.AddListener(OnclickComposite);
    }

    // 장비 선택 이벤트 트리거 하는 메서드 
    public static void TriggerSelectEquipment(Equipment equipment)
    {
        OnClickSelectEquipment?.Invoke(equipment);
    }

    // 장비 클릭 했을 때 불리는 메서드
    public void SelectEquipment(Equipment equipment)
    {
        switch (selectEquipment.type)
        {
            case EquipmentType.Weapon:
                selectEquipment.GetComponent<WeaponInfo>().SetWeaponInfo(equipment.GetComponent<WeaponInfo>());
                UpdateSelectedEquipmentUI(selectEquipment);
                break;
        }
    }

    
    private void UpdateSelectedEquipmentUI(Equipment equipment)
    {
        equipment.SetQuantityUI();

        selectEquipment.GetComponent<WeaponInfo>().SetUI();
        SetOnEquippedBtnUI(selectEquipment.OnEquipped);

        SetselectEquipmentTextUI(equipment);

    }


    // 선택 장비 데이터 UI로 보여주는 메서드
    void SetselectEquipmentTextUI(Equipment equipment)
    {
        selectEquipmentName.text = equipment.name;
        selectEquipment_equippedEffect.text = $"{BigInteger.ChangeMoney(equipment.equippedEffect.ToString())}%";
        selectEquipment_ownedEffect.text = $"{equipment.ownedEffect}%";
    }

    // 장착 버튼 활성화 / 비활성화 메서드
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

    // 강화 판넬 버튼 눌렸을 때 불리는 메서드
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

                Debug.Log("얼마냐 : " + enhanceEquipmentTemp.GetEnhanceStone());
                RequiredCurrencyText.text = enhanceEquipmentTemp.GetEnhanceStone().ToString();

                enhanceEquipment.GetComponent<WeaponInfo>().SetWeaponInfo(enhanceEquipmentTemp.GetComponent<WeaponInfo>());

                enhanceEquipment.SetUI();
                break;
        }

    }

    // 합성 버튼 눌렸을 때 불리는 메서드
    public void OnclickComposite()
    {
        EquipmentManager.instance.Composite(selectEquipment);

        selectEquipment.SetQuantityUI();

        UpdateSelectEquipmentData();
    }

    // 강화 버튼 눌렸을 때 불리는 메서드
    public void OnClickEnhance()
    {
        if (selectEquipment.enhancementLevel >= selectEquipment.enhancementMaxLevel) return;
        if (selectEquipment.GetEnhanceStone() > new BigInteger(CurrencyManager.instance.GetCurrencyAmount("EnhanceStone"))) return;
        CurrencyManager.instance.SubtractCurrency("EnhanceStone",selectEquipment.GetEnhanceStone());
        selectEquipment.Enhance();
        SetselectEquipmentTextUI(selectEquipment);


        if (selectEquipment.OnEquipped) OnClickEquip();

        UpdateSelectEquipmentData();

        OnClickEnhancePanel();
    }

    // 장착 버튼 눌렸을 때 불리는 메서드
    public void OnClickEquip()
    {
        Debug.Log("장착 됨 ");
        Player.OnEquip?.Invoke(EquipmentManager.GetEquipment(selectEquipment.name));
        
    }

    // 장착 해제 버튼 눌렀을 때 불리는 메서드
    public void OnClickUnEquip()
    {
        Player.OnUnEquip?.Invoke(selectEquipment.type);
        
    }

    // 선택한 장비 데이터 업데이트 (저장한다고 생각하면 편함)
    public void UpdateSelectEquipmentData()
    {
        EquipmentManager.SetEquipment(selectEquipment.name, selectEquipment);
    }
}
