using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInfo : Equipment
{
    [SerializeField] Image background;
    [SerializeField] Image weaponImage;
    [SerializeField] TMP_Text weaponLevelText;
    [SerializeField] int weaponCount;
    Color myColor;

    public WeaponInfo(string name, int quantity,int level, EquipmentType type, Rarity rarity, int enhancementLevel, string equippedEffect, string ownedEffect, string enhancementEffect) : base(name, quantity,level, type, rarity, enhancementLevel, equippedEffect, ownedEffect, enhancementEffect)
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

    public void SetWeaponInfo(WeaponInfo targetInfo)
    {
        this.name = targetInfo.name;
        this.quantity = targetInfo.quantity;
        this.level = targetInfo.level;
        this.type = targetInfo.type;
        this.rarity = targetInfo.rarity;
        this.enhancementLevel = targetInfo.enhancementLevel;
        this.equippedEffect = targetInfo.equippedEffect;
        this.ownedEffect = targetInfo.ownedEffect;
        this.enhancementEffect = targetInfo.enhancementEffect;
        this.myColor = targetInfo.myColor;
    }
    public void SetWeaponInfo(string name, int quantity,int level, EquipmentType type, Rarity rarity, int enhancementLevel, string equippedEffect, string ownedEffect, string enhancementEffect, Color myColor)
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
        this.myColor = myColor;

        SetUI();
    }


    public void SetUI()
    {
        SetBackgroundColor();
        SetLevelText();
    }

    void SetBackgroundColor()
    {
        Debug.Log(background);
        background.color = myColor;
    }
    void SetLevelText()
    {
        weaponLevelText.text = level.ToString();
    }
}
