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
    [SerializeField] Slider weaponQuantityBar;
    [SerializeField] TMP_Text weaponQuantityText;
    [SerializeField] int weaponCount;
    Color myColor;

    public WeaponInfo(string name, int quantity,int level, bool OnEquipped, EquipmentType type, Rarity rarity, int enhancementLevel, int basicEquippedEffect, int basicOwnedEffect) : base(name, quantity,level,OnEquipped, type, rarity, enhancementLevel, basicEquippedEffect, basicOwnedEffect)
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
    }

    public void SetWeaponInfo(WeaponInfo targetInfo)
    {
        this.name = targetInfo.name;
        this.quantity = targetInfo.quantity;
        this.level = targetInfo.level;
        this.OnEquipped = targetInfo.OnEquipped;
        this.type = targetInfo.type;
        this.rarity = targetInfo.rarity;
        this.enhancementLevel = targetInfo.enhancementLevel;
        this.basicEquippedEffect = targetInfo.basicEquippedEffect;
        this.basicOwnedEffect = targetInfo.basicOwnedEffect;
        this.myColor = targetInfo.myColor;

        equippedEffect = this.basicEquippedEffect;
        ownedEffect = this.basicOwnedEffect;
    }
    public void SetWeaponInfo(string name, int quantity,int level, bool OnEquipped, EquipmentType type, Rarity rarity, int enhancementLevel, int basicEquippedEffect, int basicOwnedEffect, Color myColor)
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
        this.myColor = myColor;

        equippedEffect = this.basicEquippedEffect;
        ownedEffect = this.basicOwnedEffect;

        SetUI();
    }

    public void SetUI()
    {
        SetBackgroundColor();
        SetLevelText();
        SetQuantityUI();
    }

    public override void SetQuantityUI()
    {
        Debug.Log("Quantity : " + quantity);
        weaponQuantityBar.value = quantity;
        weaponQuantityText.text = $"{quantity}/4";
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
