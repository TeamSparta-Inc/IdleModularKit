using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    //[SerializeField] List<Equipment> allEquipment = new List<Equipment>();

    [SerializeField] List<WeaponInfo> weapons = new List<WeaponInfo>();

    [SerializeField]
    public static Dictionary<string, Equipment> allEquipment = new Dictionary<string, Equipment>();

    WeaponInfo[] weaponss;

    Rarity[] rarities = { Rarity.Common, Rarity.Uncommon, Rarity.Rare, Rarity.Epic, Rarity.Ancient, Rarity.Legendary, Rarity.Mythology };
    //string[] rarityOrder = { "Common", "Uncommon", "Rare", "Epic", "Ancient", "Legendary", "Mythology" };

    //string[] colorsHex = { "#333333", "#3CB371", "#4169E1", "#7058A3", "#FFA500", "#C9BC46", "#DF6464" };
    [SerializeField] Color[] colors;



    int maxLevel = 4;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetAllWeapons();
    }

    void SetAllWeapons()
    {
        int weaponCount = 0;
        int rarityIntValue = 0;
        foreach(Rarity rarity in rarities)
        {
            if (rarity == Rarity.None) continue;
            rarityIntValue = Convert.ToInt32(rarity);
            for (int level =1; level <= maxLevel; level++)
            {
                WeaponInfo weapon = weapons[weaponCount];

                

                string name = $"{rarity}_{level}";// Weapon Lv
                int equippedEffect = level * ((int)Mathf.Pow(10, rarityIntValue+1));
                int ownedEffect = (int)(equippedEffect * 0.5f);
                string equippedEffectText = $"{equippedEffect}%";//Basic attack power increased by 
                string ownedEffectText = $"{ownedEffect}%"; //Overall damage increased by 

                weapon.SetWeaponInfo(name, 1, level, false, EquipmentType.Weapon, rarity,
                                 level, equippedEffect, ownedEffect, colors[rarityIntValue]);

                weapon.GetComponent<Button>().onClick.AddListener(() => EquipmentUI.TriggerSelectEquipment(weapon));

                AddEquipment(name,weapon);

                Debug.Log(colors.Length);
                //weapon.SetUI();


                weaponCount++;
            }
        }
    }


    public int Composite(Equipment equipment)
    {
        if (equipment.quantity < 4) return -1;

        int compositeCount = equipment.quantity / 4;
        equipment.quantity %= 4;

        Equipment nextEquipment = GetNextEquipment(equipment.name);

        nextEquipment.quantity += compositeCount;

        nextEquipment.SetQuantityUI();

        return compositeCount;
    }


    public static void AddEquipment(string equipmentName, Equipment equipment)
    {
        if (!allEquipment.ContainsKey(equipmentName))
        {
            allEquipment.Add(equipmentName, equipment);
        }
        else
        {
            Debug.LogWarning($"Weapon already exists in the dictionary: {equipmentName}");
        }
    }

    public static Equipment GetEquipment(string equipmentName)
    {
        if (allEquipment.TryGetValue(equipmentName, out Equipment equipment))
        {
            return equipment;
        }
        else
        {
            Debug.LogError($"Weapon not found: {equipmentName}");
            return null;
        }
    }

    public static void SetEquipment(string equipmentName, Equipment equipment)
    {
        Equipment targetEquipment = allEquipment[equipmentName];
        Debug.Log("이름 : "+ allEquipment[equipmentName].gameObject.name);
        targetEquipment.equippedEffect = equipment.equippedEffect;
        targetEquipment.ownedEffect = equipment.ownedEffect;
        targetEquipment.quantity = equipment.quantity;
        targetEquipment.OnEquipped = equipment.OnEquipped;

        targetEquipment.SetQuantityUI();
    }


    public Equipment GetNextEquipment(string currentKey)
    {
        int currentRarityIndex = -1;
        int currentLevel = -1;
        int maxLevel = 4; // 최대 레벨 설정

        // 현재 키에서 희귀도와 레벨 분리
        foreach (var rarity in rarities)
        {
            if (currentKey.StartsWith(rarity.ToString()))
            {
                currentRarityIndex = Array.IndexOf(rarities, rarity);
                int.TryParse(currentKey.Replace(rarity + "_", ""), out currentLevel);
                break;
            }
        }

        if (currentRarityIndex != -1 && currentLevel != -1)
        {
            if (currentLevel < maxLevel)
            {
                // 같은 희귀도 내에서 다음 레벨 찾기
                string nextKey = rarities[currentRarityIndex] + "_" + (currentLevel + 1);
                return allEquipment.TryGetValue(nextKey, out Equipment nextEquipment) ? nextEquipment : null;
            }
            else if (currentRarityIndex < rarities.Length - 1)
            {
                // 희귀도를 증가시키고 첫 번째 레벨의 장비 찾기
                string nextKey = rarities[currentRarityIndex + 1] + "_1";
                return allEquipment.TryGetValue(nextKey, out Equipment nextEquipment) ? nextEquipment : null;
            }
        }

        // 다음 장비를 찾을 수 없는 경우
        return null;
    }


    public Equipment GetPreviousEquipment(string currentKey)
    {
        int currentRarityIndex = -1;
        int currentLevel = -1;

        // 현재 키에서 희귀도와 레벨 분리
        foreach (var rarity in rarities)
        {
            if (currentKey.StartsWith(rarity.ToString()))
            {
                currentRarityIndex = Array.IndexOf(rarities, rarity);
                int.TryParse(currentKey.Replace(rarity + "_", ""), out currentLevel);
                break;
            }
        }

        if (currentRarityIndex != -1 && currentLevel != -1)
        {
            if (currentLevel > 1)
            {
                // 같은 희귀도 내에서 이전 레벨 찾기
                string previousKey = rarities[currentRarityIndex] + "_" + (currentLevel - 1);
                return allEquipment.TryGetValue(previousKey, out Equipment prevEquipment) ? prevEquipment : null;
            }
            else if (currentRarityIndex > 0)
            {
                // 희귀도를 낮추고 최대 레벨의 장비 찾기
                string previousKey = rarities[currentRarityIndex - 1] + "_4";
                return allEquipment.TryGetValue(previousKey, out Equipment prevEquipment) ? prevEquipment : null;
            }
        }

        // 이전 장비를 찾을 수 없는 경우
        return null;
    }

}
