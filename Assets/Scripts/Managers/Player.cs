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
    //WeaponInfo unEquiped_Weapon = null;
    //BigInteger unEquiped_WeaponEffect = 0;


    private void Awake()
    {
        instance = this;

        StatusUpgradeManager.OnAttackUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnHealthUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnDefenseUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnCritChanceUpgrade += status.IncreaseBaseStat;
        StatusUpgradeManager.OnCritDamageUpgrade += status.IncreaseBaseStat;


        OnEquip += Equip;
        OnUnEquip += UnEquip;
    }


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