using System;
using System.Collections;
using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class statusUpgradeManager : MonoBehaviour
{
    public static event Action<int> _onAttackUpgrade;
    public static event Action<int> _onHealthUpgrade;
    public static event Action<int> _onDefenseUpgrade;
    public static event Action<float> _onCritChanceUpgrade;
    public static event Action<float> _onCritDamageUpgrade;

#region UpgradeData
    int attackUpgradeLevel
    {
        get
        {
            return PlayerPrefs.GetInt("AttackUpgradeLevel",1);
        }
        set
        {
            PlayerPrefs.SetInt("AttackUpgradeLevel", value);
        }
    } 
    BigInteger attackUpgradePrice = 150; // [픽셀헌터] 1(180) -> 20(450) /// [현재] 1(50) -> 20() 
    BigInteger attackUpgradeValue = 0; // 
    int attackPricePercent = 3;
    int increaseAttack = 2;

    int healthUpgradeLevel
    {
        get
        {
            return PlayerPrefs.GetInt("HealthUpgradeLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("HealthUpgradeLevel", value);
        }
    }
    BigInteger healthUpgradePrice = 150;
    BigInteger healthUpgradeValue = 0;
    int healthPricePercent = 3;
    int increaseHealth = 50;

    int defenseUpgradeLevel
    {
        get
        {
            return PlayerPrefs.GetInt("DefenseUpgradeLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("DefenseUpgradeLevel", value);
        }
    }
    BigInteger defenseUpgradePrice = 200;
    BigInteger defenseUpgradeValue = 0;
    int defensePricePercent = 4;
    int increaseDefense = 2;

    int critChanceUpgradeLevel
    {
        get
        {
            return PlayerPrefs.GetInt("CritChanceUpgradeLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("CritChanceUpgradeLevel", value);
        }
    }
    BigInteger critChanceUpgradePrice = 500;
    float critChanceUpgradeValue = 0;
    int critChancePricePercent = 6;
    float increaseCritChance = 0.1f;

    int critDamageUpgradeLevel
    {
        get
        {
            return PlayerPrefs.GetInt("CritDamageUpgradeLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("CritDamageUpgradeLevel", value);
        }
    }
    BigInteger critDamageUpgradePrice = 500;
    float critDamageUpgradeValue = 100f;
    int critDamagePricePercent = 4;
    float increaseCritDamage = 10f;
    #endregion


    [Header("[공격력]")]
    [SerializeField] TMP_Text attackUpgradeLevelText;
    [SerializeField] TMP_Text attackUpgradeValueText;
    [SerializeField] TMP_Text attackUpgradePriceText;
    [SerializeField] Button attackUpgradeBtn;

    [Header("[체력]")]
    [SerializeField] TMP_Text healthUpgradeLevelText;
    [SerializeField] TMP_Text healthUpgradeValueText;
    [SerializeField] TMP_Text healthUpgradePriceText;
    [SerializeField] Button healthUpgradeBtn;

    [Header("[방어력]")]
    [SerializeField] TMP_Text defenseUpgradeLevelText;
    [SerializeField] TMP_Text defenseUpgradeValueText;
    [SerializeField] TMP_Text defenseUpgradePriceText;
    [SerializeField] Button defenseUpgradeBtn;

    [Header("[치명타 확률]")]
    [SerializeField] TMP_Text critChanceUpgradeLevelText;
    [SerializeField] TMP_Text critChanceUpgradeValueText;
    [SerializeField] TMP_Text critChanceUpgradePriceText;
    [SerializeField] Button critChanceUpgradeBtn;

    [Header("[치명타 데미지]")]
    [SerializeField] TMP_Text critDamageUpgradeLevelText;
    [SerializeField] TMP_Text critDamageUpgradeValueText;
    [SerializeField] TMP_Text critDamageUpgradePriceText;
    [SerializeField] Button critDamageUpgradeBtn;



    private void Start()
    {
        attackUpgradeBtn.onClick.AddListener(UpgradeAttack);
        healthUpgradeBtn.onClick.AddListener(UpgradeHealth);
        defenseUpgradeBtn.onClick.AddListener(UpgradeDefense);
        critChanceUpgradeBtn.onClick.AddListener(UpgradeCritChance);
        critDamageUpgradeBtn.onClick.AddListener(UpgradeCritDamage);
        SetUpgradeUI_ALL();
    }


    void SetUpgradeUI_ALL()
    {
        SetUpgradeUI_Attack();
        SetUpgradeUI_Health();
        SetUpgradeUI_Defense();
        SetUpgradeUI_CritChance();
        SetUpgradeUI_CritDamage();
    }

    void SetUpgradeUI_Attack()
    {
        attackUpgradeLevelText.text = $"{attackUpgradeLevel}";
        attackUpgradePriceText.text = $"{attackUpgradePrice}";
        attackUpgradeValueText.text = $"{attackUpgradeValue}";
    }

    void SetUpgradeUI_Health()
    {
        healthUpgradeLevelText.text = $"{healthUpgradeLevel}";
        healthUpgradePriceText.text = $"{healthUpgradePrice}";
        healthUpgradeValueText.text = $"{healthUpgradeValue}";
    }

    void SetUpgradeUI_Defense()
    {
        defenseUpgradeLevelText.text = $"{defenseUpgradeLevel}";
        defenseUpgradePriceText.text = $"{defenseUpgradePrice}";
        defenseUpgradeValueText.text = $"{defenseUpgradeValue}";
    }

    void SetUpgradeUI_CritChance()
    {
        critChanceUpgradeLevelText.text = $"{critChanceUpgradeLevel}";
        critChanceUpgradePriceText.text = $"{critChanceUpgradePrice}";
        critChanceUpgradeValueText.text = $"{critChanceUpgradeValue}";
    }

    void SetUpgradeUI_CritDamage()
    {
        critDamageUpgradeLevelText.text = $"{critDamageUpgradeLevel}";
        critDamageUpgradePriceText.text = $"{critDamageUpgradePrice}";
        critDamageUpgradeValueText.text = $"{critDamageUpgradeValue}";
    }




    public void UpgradeAttack()
    {
        attackUpgradeLevel++;
        attackUpgradeValue += increaseAttack;
        attackUpgradePrice = attackUpgradePrice + (attackUpgradePrice / 100 * attackPricePercent);
        _onAttackUpgrade?.Invoke(increaseAttack);
        SetUpgradeUI_Attack();
    }

    public void UpgradeHealth()
    {
        healthUpgradeLevel++;
        healthUpgradeValue += increaseHealth;
        healthUpgradePrice = healthUpgradePrice + (healthUpgradePrice / 100 * healthPricePercent);
        _onHealthUpgrade?.Invoke(increaseHealth);
        SetUpgradeUI_Health();
    }

    public void UpgradeDefense()
    {
        defenseUpgradeLevel++;
        defenseUpgradeValue += increaseDefense;
        defenseUpgradePrice = defenseUpgradePrice + (defenseUpgradePrice / 100 * defensePricePercent);
        _onDefenseUpgrade?.Invoke(increaseDefense);
        SetUpgradeUI_Defense();
    }

    public void UpgradeCritChance()
    {
        critChanceUpgradeLevel++;
        critChanceUpgradeValue += increaseCritChance;
        critChanceUpgradePrice = critChanceUpgradePrice + (critChanceUpgradePrice / 100 * critChancePricePercent);
        _onCritChanceUpgrade?.Invoke(increaseCritChance);
        SetUpgradeUI_CritChance();
    }

    public void UpgradeCritDamage()
    {
        critDamageUpgradeLevel++;
        critDamageUpgradeValue += increaseCritDamage;
        critDamageUpgradePrice = critDamageUpgradePrice + (critDamageUpgradePrice / 100 * critDamagePricePercent);
        _onCritDamageUpgrade?.Invoke(increaseCritDamage);
        SetUpgradeUI_CritDamage();
    }
}
