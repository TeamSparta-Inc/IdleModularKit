using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    [SerializeField] List<Equipment> allEquipment = new List<Equipment>();

    [SerializeField] List<WeaponInfo> weapons = new List<WeaponInfo>();
    WeaponInfo[] weaponss;

    Rarity[] rarities = { Rarity.Common, Rarity.Uncommon, Rarity.Rare, Rarity.Epic, Rarity.Ancient, Rarity.Legendary, Rarity.Mythology };

    //string[] colorsHex = { "#333333", "#3CB371", "#4169E1", "#7058A3", "#FFA500", "#C9BC46", "#DF6464" };
    [SerializeField] Color[] colors;

    int maxLevel = 4;


    private void Start()
    {
        SetAllWeapons();
    }

    void SetAllWeapons()
    {
        int weaponCount = 0;
        foreach(Rarity rarity in rarities)
        {
            for (int level =1; level <= maxLevel; level++)
            {
                WeaponInfo weapon = weapons[weaponCount];

                string name = $"{rarity}_{level}";// Weapon Lv
                string equippedEffect = $"{level * 10}%";//Basic attack power increased by 
                string ownedEffect = $"{level * 2}%"; //Overall damage increased by 
                string enhancementEffect = $"At Lv{level * 50}, attack power increased by {level * 100}%";


                weapon.SetWeaponInfo(name, 1,level, EquipmentType.Weapon, rarity,
                                 level, equippedEffect, ownedEffect, enhancementEffect, colors[Convert.ToInt32(rarity)]);

                weapon.GetComponent<Button>().onClick.AddListener(() => EquipmentUI.TriggerSelectEquipment(weapon));


                allEquipment.Add(weapon);

                Debug.Log(colors.Length);
                //weapon.SetUI();


                weaponCount++;
            }
        }
    } 
}
