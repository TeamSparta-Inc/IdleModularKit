using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장비 타입
public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory
    // 기타 장비 타입...
}

// 희귀도 
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Ancient,
    Legendary,
    Mythology,
    None
    // 기타 희귀도...
}


public class Equipment : MonoBehaviour
{
    public string name;          // 장비의 이름
    public int quantity;         // 장비의 개수
    public int level;
    public EquipmentType type;   // 장비의 타입 (예: 무기, 방어구 등)
    public Rarity rarity;        // 장비의 희귀도
    public int enhancementLevel; // 강화 상태 (예: 0, 1, 2, ...)
    public int equippedEffect;  // 장착효과
    public int ownedEffect;     // 보유효과
    public string enhancementEffect; // 강화효과


    public Equipment(string name, int quantity, int level, EquipmentType type, Rarity rarity,
                 int enhancementLevel, int equippedEffect, int ownedEffect, string enhancementEffect)
    {
        this.name = name;
        this.quantity = quantity;
        this.level = level;
        this.type = type;
        this.rarity = rarity;
        this.enhancementLevel = enhancementLevel;
        this.equippedEffect = equippedEffect;
        this.ownedEffect = ownedEffect;
        this.enhancementEffect = enhancementEffect;
    }

    // 강화 메서드
    public virtual void Enhance()
    {
        // 강화 로직...
        enhancementLevel++;
        // 강화효과 업데이트...
    }



    public float CalculateEffect(float baseStat)
    {
        float effect = 0f;
        switch (type)
        {
            case EquipmentType.Weapon:
                // 무기의 경우
                effect = baseStat + (enhancementLevel * baseStat * 0.05f); // 장착 효과
                effect += 10; // 보유 효과
                break;
            case EquipmentType.Armor:
                // 방어구의 경우
                effect = baseStat + (enhancementLevel * baseStat * 0.03f); // 장착 효과
                effect += 20; // 보유 효과
                break;
        }
        return effect;
    }

    public bool CheckQuantity()
    {
        if (quantity >= 4)
        {
            return true;
        }

        SetQuantityUI();
        return false;
    }

    public virtual void SetQuantityUI()
    {
    }
}
