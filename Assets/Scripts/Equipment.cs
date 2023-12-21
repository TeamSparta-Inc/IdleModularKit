using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keiwando.BigInteger;

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
    public bool OnEquipped;
    public EquipmentType type;   // 장비의 타입 (예: 무기, 방어구 등)
    public Rarity rarity;        // 장비의 희귀도
    public int enhancementLevel; // 강화 상태 (예: 0, 1, 2, ...)
    public int basicEquippedEffect;
    public BigInteger equippedEffect;  // 장착효과
    public int basicOwnedEffect;
    public BigInteger ownedEffect;     // 보유효과
   [HideInInspector] public int enhancementMaxLevel = 100;

    public Equipment(string name, int quantity, int level, bool OnEquipped, EquipmentType type, Rarity rarity,
                 int enhancementLevel, int basicEquippedEffect, int basicOwnedEffect)
    {
        this.name = name;
        this.quantity = quantity;
        this.level = level;
        this.OnEquipped = OnEquipped;
        this.type = type;
        this.rarity = rarity;
        this.enhancementLevel = enhancementLevel;
        this.basicEquippedEffect = basicEquippedEffect;
        this.basicOwnedEffect = basicOwnedEffect;

        equippedEffect = this.basicEquippedEffect;
        ownedEffect = this.basicOwnedEffect;
    }

    // 강화 메서드
    public virtual void Enhance()
    {
        // 강화 로직...
        equippedEffect += basicEquippedEffect;
        ownedEffect += basicOwnedEffect;

        enhancementLevel++;
        // 강화효과 업데이트...
    }

    // 강화할 때 필요한 강화석 return 시키는 메서드
    public BigInteger GetEnhanceStone()
    {
        Debug.Log($"{ownedEffect}  {basicOwnedEffect}");
        var requipredEnhanceStone = equippedEffect - basicOwnedEffect;

        return requipredEnhanceStone;
    }

    // 개수 체크하는 메서드
    public bool CheckQuantity()
    {
        if (quantity >= 4)
        {
            return true;
        }

        SetQuantityUI();
        return false;
    }

    // WeaponInfo 확인.
    public virtual void SetQuantityUI(){}
    public virtual void SetUI(){}

    // 장비 데이터를 ES3 파일에 저장
    public void SaveEquipment()
    {
        Debug.Log("장비 정보 저장 " + name);

        ES3.Save<string>("name_" + name, name);
        ES3.Save<int>("quantity_" + name, quantity);
        ES3.Save<int>("level_" + name, level);
        ES3.Save<bool>("onEquipped_" + name, OnEquipped);
        ES3.Save<EquipmentType>("type_" + name, type);
        ES3.Save<Rarity>("rarity_" + name, rarity);
        ES3.Save<int>("enhancementLevel_"+ name, enhancementLevel);
        ES3.Save<int>("basicEquippedEffect_" + name, basicEquippedEffect);
        ES3.Save<int>("basicOwnedEffect_" + name, basicOwnedEffect);

        ES3.Save<string>("equippedEffect_" + name, equippedEffect.ToString());
        ES3.Save<string>("ownedEffect_" + name, ownedEffect.ToString());
    }
    public void SaveEquipment(string equipmentID)
    {
        Debug.Log("장비 정보 저장 " + equipmentID);

        ES3.Save<string>("name_" + equipmentID, name);
        ES3.Save<int>("quantity_" + equipmentID, quantity);
        ES3.Save<int>("level_" + equipmentID, level);
        ES3.Save<bool>("onEquipped_" + equipmentID, OnEquipped);
        ES3.Save<EquipmentType>("type_" + equipmentID, type);
        ES3.Save<Rarity>("rarity_" + equipmentID, rarity);
        ES3.Save<int>("enhancementLevel_"+ equipmentID, enhancementLevel);
        ES3.Save<int>("basicEquippedEffect_" + equipmentID, basicEquippedEffect);
        ES3.Save<int>("basicOwnedEffect_" + equipmentID, basicOwnedEffect);

        ES3.Save<string>("equippedEffect_" + equipmentID, equippedEffect.ToString());
        ES3.Save<string>("ownedEffect_" + equipmentID, ownedEffect.ToString());
    }

    // 장비 데이터를 ES3 파일에서 불러오기
    public void LoadEquipment()
    {
        if (!ES3.KeyExists("name_" + name)) return;

        Debug.Log("장비 정보 불러오기 " + name);

        name = ES3.Load<string>("name_" + name);
        quantity = ES3.Load<int>("quantity_" + name);
        level = ES3.Load<int>("level_" + name);
        OnEquipped = ES3.Load<bool>("onEquipped_" + name);
        type = ES3.Load<EquipmentType>("type_" + name);
        rarity = ES3.Load<Rarity>("rarity_" + name);
        enhancementLevel = ES3.Load<int>("enhancementLevel_" + name);
        basicEquippedEffect = ES3.Load<int>("basicEquippedEffect_" + name);
        basicOwnedEffect = ES3.Load<int>("basicOwnedEffect_" + name);

        equippedEffect = new BigInteger(ES3.Load<string>("equippedEffect_" + name));
        ownedEffect = new BigInteger(ES3.Load<string>("ownedEffect_" + name));

    }
    public void LoadEquipment(string equipmentID)
    {
        if (!ES3.KeyExists("name_" + equipmentID)) return;

        Debug.Log("장비 정보 불러오기 " + equipmentID);

        name = ES3.Load<string>("name_" + equipmentID);
        quantity = ES3.Load<int>("quantity_" + equipmentID);
        level = ES3.Load<int>("level_" + equipmentID);
        OnEquipped = ES3.Load<bool>("onEquipped_" + equipmentID);
        type = ES3.Load<EquipmentType>("type_" + equipmentID);
        rarity = ES3.Load<Rarity>("rarity_" + equipmentID);
        enhancementLevel = ES3.Load<int>("enhancementLevel_" + equipmentID);
        basicEquippedEffect = ES3.Load<int>("basicEquippedEffect_" + equipmentID);
        basicOwnedEffect = ES3.Load<int>("basicOwnedEffect_" + equipmentID);

        equippedEffect = new BigInteger(ES3.Load<string>("equippedEffect_" + equipmentID));
        ownedEffect = new BigInteger(ES3.Load<string>("ownedEffect_" + equipmentID));

    }
}
