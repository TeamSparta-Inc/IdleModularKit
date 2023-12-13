using System;
using System.Collections;
using System.Collections.Generic;
using Keiwando.BigInteger;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

enum StatusType { ATK, HP, DEF, CRIT_CH, CRIT_DMG }

struct UpgradeData
{
    public StatusType statusType;

    public int upgradeLevel;
    public BigInteger upgradePrice;
    public BigInteger upgradeValue;
    public int pricePercent;
    public int increase;

    public Action<int> OnStatusUpgrade;

    public float critUpgradeValue;
    public float critIncrease;
    public Action<float> OnCritStatusUpgrade;

    public TMP_Text upgradeLevelText;
    public TMP_Text upgradeValueText;
    public TMP_Text upgradePriceText;
    public Button upgradeBtn;

    public void SetUpgradeUI()
    {
        upgradeLevelText.text = $"{upgradeLevel}";
        upgradePriceText.text = $"{upgradePrice.ChangeMoney()}";
        if (upgradeValue == null) {
            upgradeValueText.text = $"{critUpgradeValue:0.00}";
            return;
        }
        upgradeValueText.text = $"{upgradeValue.ChangeMoney()}";
    }

    public void StatusUpdate()
    {
        upgradeLevel++;
        upgradeValue += increase;
        upgradePrice = upgradePrice + (upgradePrice / 100 * pricePercent);

        PlayerPrefs.SetInt($"{statusType}UpgradeLevel", upgradeLevel);
        OnStatusUpgrade?.Invoke(increase);
    }

    public void CritStatusUpdate()
    {
        upgradeLevel++;
        critUpgradeValue += critIncrease;
        upgradePrice = upgradePrice + (upgradePrice / 100 * pricePercent);

        PlayerPrefs.SetInt($"{statusType}UpgradeLevel", upgradeLevel);
        OnCritStatusUpgrade?.Invoke(critIncrease);
    }


    public void LoadLevelforStatus()
    {
        for (int i=0; i<upgradeLevel; i++)
        {
            upgradeValue += increase;
            upgradePrice = upgradePrice + (upgradePrice / 100 * pricePercent);

            OnStatusUpgrade?.Invoke(increase);
        }
    }

    public void LoadLevelforCritStatus()
    {
        for (int i=0; i<upgradeLevel; i++)
        {
            critUpgradeValue += critIncrease;
            upgradePrice = upgradePrice + (upgradePrice / 100 * pricePercent);

            OnCritStatusUpgrade?.Invoke(critIncrease);
        }
    }


    public UpgradeData(StatusType statusType, int upgradeLevel, BigInteger upgradePrice, int pricePercent,
        TMP_Text upgradeLevelText, TMP_Text upgradeValueText, TMP_Text upgradePriceText, Button upgradeBtn,
        int increase = 0, BigInteger upgradeValue = null, Action<int> OnStatusUpgrade = null, float critIncrease = 0, float critUpgradeValue = 0, Action<float> OnCritStatusUpgrade = null)
    {
        this.statusType = statusType;
        this.upgradeLevel = upgradeLevel;
        this.upgradePrice = upgradePrice;
        this.upgradeValue = upgradeValue;
        this.pricePercent = pricePercent;
        this.increase = increase;

        this.OnStatusUpgrade = OnStatusUpgrade;

        this.upgradeLevelText = upgradeLevelText;
        this.upgradeValueText = upgradeValueText;
        this.upgradePriceText = upgradePriceText;
        this.upgradeBtn = upgradeBtn;


        this.critUpgradeValue = critUpgradeValue;
        this.critIncrease = critIncrease;
        this.OnCritStatusUpgrade = OnCritStatusUpgrade;

        if (statusType == StatusType.CRIT_CH || statusType == StatusType.CRIT_DMG)
            LoadLevelforCritStatus();
        else
            LoadLevelforStatus();
        
    }
}



public class statusUpgradeManager : MonoBehaviour
{
    public static event Action<int> OnAttackUpgrade;
    public static event Action<int> OnHealthUpgrade;
    public static event Action<int> OnDefenseUpgrade;
    public static event Action<float> OnCritChanceUpgrade;
    public static event Action<float> OnCritDamageUpgrade;

    [Header("[능력치 조정]")]
    [Header("[공격력]")]
    [Header("[처음 가격, 스텟 상승치, 가격 상승률]")]
    [SerializeField] int attackFirstPrice = 150;
    [SerializeField] int attackincrease = 2;
    [SerializeField] int attackpricePercent = 15;

    [Header("[체력]")]
    [Header("[처음 가격, 스텟 상승치, 가격 상승률]")]
    [SerializeField] int healthFirstPrice = 150;
    [SerializeField] int healthincrease = 50;
    [SerializeField] int healthpricePercent = 15;

    [Header("[방어력]")]
    [Header("[처음 가격, 스텟 상승치, 가격 상승률]")]
    [SerializeField] int defenseFirstPrice = 200;
    [SerializeField] int defenseincrease = 2;
    [SerializeField] int defensepricePercent = 17;

    [Header("[크리티컬 확률]")]
    [Header("[처음 가격, 스텟 상승치, 가격 상승률]")]
    [SerializeField] int critChanceFirstPrice = 500;
    [SerializeField] float critChanceincrease = 0.1f;
    [SerializeField] int critChancepricePercent = 25;

    [Header("[크리티컬 데미지]")]
    [Header("[처음 가격, 스텟 상승치, 가격 상승률]")]
    [SerializeField] int critDamageFirstPrice = 500;
    [SerializeField] int critDamageincrease = 10;
    [SerializeField] int critDamagepricePercent = 18;



    [Header("업그레이드 UI")]
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



    UpgradeData attackUpgradeData;
    UpgradeData healthUpgradeData;
    UpgradeData defenseUpgradeData;
    UpgradeData critChanceUpgradeData;
    UpgradeData critDamageUpgradeData;


    private void Start()
    {

        InitializeButtonListeners();
        InitializeUpgradeData();
        SetUpgradeUI_ALL();
    }

    // 버튼 초기화 메서드
    void InitializeButtonListeners()
    {
        attackUpgradeBtn.onClick.AddListener(UpgradeAttack);
        healthUpgradeBtn.onClick.AddListener(UpgradeHealth);
        defenseUpgradeBtn.onClick.AddListener(UpgradeDefense);
        critChanceUpgradeBtn.onClick.AddListener(UpgradeCritChance);
        critDamageUpgradeBtn.onClick.AddListener(UpgradeCritDamage);
    }

    // UpdateData 초기화 메서드 - 여기서 스텟퍼센트 조정 가능
    void InitializeUpgradeData()
    {
        attackUpgradeData = new UpgradeData(
            StatusType.ATK,
            PlayerPrefs.GetInt($"{StatusType.ATK}UpgradeLevel", 0),
            attackFirstPrice,
            attackpricePercent,
            increase: attackincrease,
            upgradeValue: 0,
            OnStatusUpgrade: OnAttackUpgrade,
            upgradeLevelText: attackUpgradeLevelText,
            upgradeValueText: attackUpgradeValueText,
            upgradePriceText: attackUpgradePriceText,
            upgradeBtn: attackUpgradeBtn);
        healthUpgradeData = new UpgradeData(
            StatusType.HP,
            PlayerPrefs.GetInt($"{StatusType.HP}UpgradeLevel", 0),
            healthFirstPrice,
            healthpricePercent,
            increase: healthincrease,
            upgradeValue: 0,
            OnStatusUpgrade: OnHealthUpgrade,
            upgradeLevelText: healthUpgradeLevelText,
            upgradeValueText: healthUpgradeValueText,
            upgradePriceText: healthUpgradePriceText,
            upgradeBtn: healthUpgradeBtn
            );
        defenseUpgradeData = new UpgradeData(
            StatusType.DEF,
            PlayerPrefs.GetInt($"{StatusType.DEF}UpgradeLevel", 0),
            defenseFirstPrice,
            defensepricePercent,
            increase: defenseincrease,
            upgradeValue: 0,
            OnStatusUpgrade: OnDefenseUpgrade,
            upgradeLevelText: defenseUpgradeLevelText,
            upgradeValueText: defenseUpgradeValueText,
            upgradePriceText: defenseUpgradePriceText,
            upgradeBtn: defenseUpgradeBtn);
        critChanceUpgradeData = new UpgradeData(
            StatusType.CRIT_CH,
            PlayerPrefs.GetInt($"{StatusType.CRIT_CH}UpgradeLevel", 0),
            critChanceFirstPrice,
            critChancepricePercent,
            critIncrease: critChanceincrease,
            critUpgradeValue: 0f,
            OnCritStatusUpgrade: OnCritChanceUpgrade,
            upgradeLevelText: critChanceUpgradeLevelText,
            upgradeValueText: critChanceUpgradeValueText,
            upgradePriceText: critChanceUpgradePriceText,
            upgradeBtn: critChanceUpgradeBtn
            );
        critDamageUpgradeData = new UpgradeData(
            StatusType.CRIT_DMG,
            PlayerPrefs.GetInt($"{StatusType.CRIT_DMG}UpgradeLevel", 0),
            critDamageFirstPrice,
            critDamagepricePercent,
            critIncrease: critDamageincrease,
            critUpgradeValue: 100f,
            OnCritStatusUpgrade: OnCritDamageUpgrade,
            upgradeLevelText: critDamageUpgradeLevelText,
            upgradeValueText: critDamageUpgradeValueText,
            upgradePriceText: critDamageUpgradePriceText,
            upgradeBtn: critDamageUpgradeBtn
            );
    }



    // UI 업데이트
    void SetUpgradeUI(UpgradeData upgradeData) => upgradeData.SetUpgradeUI();

    void SetUpgradeUI(StatusType type)
    {
        switch (type)
        {
            case StatusType.ATK:
                attackUpgradeData.SetUpgradeUI();
                break;
            case StatusType.HP:
                healthUpgradeData.SetUpgradeUI();
                break;
            case StatusType.DEF:
                defenseUpgradeData.SetUpgradeUI();
                break;
            case StatusType.CRIT_CH:
                critChanceUpgradeData.SetUpgradeUI();
                break;
            case StatusType.CRIT_DMG:
                critDamageUpgradeData.SetUpgradeUI();
                break;
        }
    }

    void SetUpgradeUI_ALL()
    {
        attackUpgradeData.SetUpgradeUI();
        healthUpgradeData.SetUpgradeUI();
        defenseUpgradeData.SetUpgradeUI();
        critChanceUpgradeData.SetUpgradeUI();
        critDamageUpgradeData.SetUpgradeUI();
    }




    // 버튼 눌렸을 때 동작하는 메서드
    public void UpgradeAttack()
    {
        attackUpgradeData.StatusUpdate();
        SetUpgradeUI(attackUpgradeData);
    }

    public void UpgradeHealth()
    {
        healthUpgradeData.StatusUpdate();
        SetUpgradeUI(healthUpgradeData);
    }

    public void UpgradeDefense()
    {
        defenseUpgradeData.StatusUpdate();
        SetUpgradeUI(defenseUpgradeData);
    }

    public void UpgradeCritChance()
    {
        critChanceUpgradeData.CritStatusUpdate();
        SetUpgradeUI(critChanceUpgradeData);
    }

    public void UpgradeCritDamage()
    {
        critDamageUpgradeData.CritStatusUpdate();
        SetUpgradeUI(critDamageUpgradeData);
    }
}
