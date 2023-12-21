using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keiwando.BigInteger;
using System;

public class Player : MonoBehaviour
{
    public static Action<Equipment> OnEquip;
    public static Action<EquipmentType> OnUnEquip;

    public static Player instance;

    [SerializeField]
    PlayerStatus status;


    [SerializeField]
    [Header("총 공격력")]
    private BigInteger currentAttack;
    [SerializeField][Header("총 체력")]
    private BigInteger currentHealth;
    [SerializeField][Header("총 방어력")]
    private BigInteger currentDefense;
    [SerializeField][Header("총 크리티컬 확률")]
    private BigInteger currentCritChance;
    [SerializeField][Header("총 크리티컬 데미지")]
    private BigInteger currentCritDamage;

    [SerializeField]
    WeaponInfo equiped_Weapon = null;


    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        SetupEventListeners();
    }

    // 이벤트 설정하는 메서드
    void SetupEventListeners()
    {
        StatusUpgradeManager.OnAttackUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnHealthUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnDefenseUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnCritChanceUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnCritDamageUpgrade += status.IncreaseBaseStat;


        OnEquip += Equip;
        OnUnEquip += UnEquip;
    }

    // 현재 능력치를 불러오는 메서드
    public BigInteger GetCurrentStatus(StatusType statusType)
    {
        switch (statusType)
        {
            case StatusType.ATK:
                return currentAttack;
            case StatusType.HP:
                return currentHealth;
            case StatusType.DEF:
                return currentDefense;
            case StatusType.CRIT_CH:
                return currentCritChance;
            case StatusType.CRIT_DMG:
                return currentCritDamage;
        }
        return null;
    }

    // 현재 능력치를 업데이트 하는 메서드
    public void SetCurrentStatus(StatusType statusType, BigInteger statusValue)
    {
        switch (statusType)
        {
            case StatusType.ATK:
                Debug.Log("강화 됨! " + statusValue );
                currentAttack = statusValue;
                break;
            case StatusType.HP:
                currentHealth = statusValue;
                break;
            case StatusType.DEF:
                currentDefense = statusValue;
                break;
            case StatusType.CRIT_CH:
                currentCritChance = statusValue;
                break;
            case StatusType.CRIT_DMG:
                currentCritDamage = statusValue;
                break;
        }
    }

    // 장비 장착하는 메서드 
    public void Equip(Equipment equipment)
    {
        //equipment.OnEquipped = true;
        switch(equipment.type)
        {
            case EquipmentType.Weapon:

                UnEquip(equipment.type);
                
                equiped_Weapon = equipment.GetComponent<WeaponInfo>();

                equiped_Weapon.OnEquipped = true;

                status.IncreaseBaseStatByPercent(StatusType.ATK, equiped_Weapon.equippedEffect);

                EquipmentUI.UpdateEquipmentUI?.Invoke(equiped_Weapon.OnEquipped);
                equiped_Weapon.SaveEquipment();
                Debug.Log("장비 장착" + equiped_Weapon.name);
                break;
        }
    }

    // 장비 장착 해제하는 메서드 
    public void UnEquip(EquipmentType equipmentType)
    {
        // 퍼센트 차감 로직 구현 필요.
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                if (equiped_Weapon == null) return;
                equiped_Weapon.OnEquipped = false;
                EquipmentUI.UpdateEquipmentUI?.Invoke(equiped_Weapon.OnEquipped);
                status.DecreaseBaseStatByPercent(StatusType.ATK, equiped_Weapon.equippedEffect);
                equiped_Weapon.SaveEquipment();
                Debug.Log("장비 장착 해제" + equiped_Weapon.name);
                equiped_Weapon = null;
                break;
        }
    }
}
