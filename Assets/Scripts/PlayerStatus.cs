using System.Collections.Generic;
using UnityEngine;
using Keiwando.BigInteger;
using System;


[Serializable]
public class PlayerStatus : BaseStatus
{
    [SerializeField]
    int attackPercent = 0;
    BigInteger currentAttackValue = 0;

    [SerializeField]
    int healthPercent = 0;
    BigInteger currentHealthValue = 0;

    [SerializeField]
    int defensePercent = 0;
    BigInteger currentDefenseValue = 0;

    [SerializeField]
    int critChancePercent = 0;
    BigInteger currentCritChanceValue = 0;

    [SerializeField]
    int critDamagePercent = 0;
    BigInteger currentCritDamageValue = 0;



    public void IncreaseBaseStat(StatusType statsType, int addValue)
    {
        Debug.Log("누구냐 넌 : " + statsType);
        switch (statsType)
        {
            case StatusType.ATK:
                Player.instance.SetCurrentStatus(statsType, IncreaseBaseStat(ref baseAttack, addValue, ref currentAttackValue, attackPercent));
                break;
            case StatusType.HP:
                Player.instance.SetCurrentStatus(statsType, IncreaseBaseStat(ref baseHealth, addValue, ref currentHealthValue, healthPercent));
                break;
            case StatusType.DEF:
                Player.instance.SetCurrentStatus(statsType, IncreaseBaseStat(ref baseDefense, addValue, ref currentDefenseValue, defensePercent));
                break;
        }
        return;
    }
    public void IncreaseBaseStat(StatusType statsType, float addValue)
    {
        switch (statsType)
        {
            case StatusType.CRIT_CH:
                Player.instance.SetCurrentStatus(statsType, IncreaseBaseStat(ref baseCritChance, addValue, ref currentCritChanceValue, critChancePercent));
                break;
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statsType, IncreaseBaseStat(ref baseCritDamage, addValue, ref currentCritDamageValue, critDamagePercent));
                break;
        }
        return;
    }

    public void DecreaseBaseStat(StatusType statsType, int subtractValue)
    {
        Debug.Log("누구냐 넌 : " + statsType);
        switch (statsType)
        {
            case StatusType.ATK:
                Player.instance.SetCurrentStatus(statsType, DecreaseBaseStat(ref baseAttack, subtractValue, ref currentAttackValue, attackPercent));
                break;
            case StatusType.HP:
                Player.instance.SetCurrentStatus(statsType, DecreaseBaseStat(ref baseHealth, subtractValue, ref currentHealthValue, healthPercent));
                break;
            case StatusType.DEF:
                Player.instance.SetCurrentStatus(statsType, DecreaseBaseStat(ref baseDefense, subtractValue, ref currentDefenseValue, defensePercent));
                break;
        }
        return;
    }
    public void DecreaseBaseStat(StatusType statsType, float subtractValue)
    {
        switch (statsType)
        {
            case StatusType.CRIT_CH:
                Player.instance.SetCurrentStatus(statsType, DecreaseBaseStat(ref baseCritChance, subtractValue, ref currentCritChanceValue, critChancePercent));
                break;
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statsType, DecreaseBaseStat(ref baseCritDamage, subtractValue, ref currentCritDamageValue, critDamagePercent));
                break;
        }
        return;
    }


    public void IncreaseBaseStatByPercent(StatusType statusType, int addPercent)
    {
        switch (statusType)
        {
            case StatusType.ATK:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStatByPercent(ref attackPercent, addPercent, ref currentAttackValue, baseAttack));
                break;
            case StatusType.HP:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStatByPercent(ref healthPercent, addPercent, ref currentHealthValue, baseHealth));
                break;
            case StatusType.DEF:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStatByPercent(ref defensePercent, addPercent, ref currentDefenseValue, baseDefense));
                break;
            case StatusType.CRIT_CH:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStatByPercent(ref critChancePercent, addPercent, ref currentCritChanceValue, baseCritChance));
                break;
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStatByPercent(ref critDamagePercent, addPercent, ref currentCritDamageValue, baseCritDamage));
                break;
        }
    }

    public void DecreaseBaseStatByPercent(StatusType statusType, int subtractPercent)
    {
        switch (statusType)
        {
            case StatusType.ATK:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStatByPercent(ref attackPercent, subtractPercent, ref currentAttackValue, baseAttack));
                break;
            case StatusType.HP:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStatByPercent(ref healthPercent, subtractPercent, ref currentHealthValue, baseHealth));
                break;
            case StatusType.DEF:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStatByPercent(ref defensePercent, subtractPercent, ref currentDefenseValue, baseDefense));
                break;
            case StatusType.CRIT_CH:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStatByPercent(ref critChancePercent, subtractPercent, ref currentCritChanceValue, baseCritChance));
                break;
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStatByPercent(ref critDamagePercent, subtractPercent, ref currentCritDamageValue, baseCritDamage));
                break;
        }
    }


    BigInteger IncreaseBaseStat(ref int baseStat, int addValue, ref BigInteger currentValue, int percent)
    {
        baseStat += addValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }

    // 임시
    BigInteger IncreaseBaseStat(ref float baseStat, float addValue, ref BigInteger currentValue, int percent)
    {
        baseStat += addValue;
        return CalculateTotal(((int)baseStat), percent, ref currentValue);
    }


    BigInteger DecreaseBaseStat(ref int baseStat, int subtractValue, ref BigInteger currentValue, int percent)
    {
        baseStat -= subtractValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }

    // 임시
    BigInteger DecreaseBaseStat(ref float baseStat, float subtractValue, ref BigInteger currentValue, int percent)
    {
        baseStat -= subtractValue;
        return CalculateTotal(((int)baseStat), percent, ref currentValue);
    }

    

    BigInteger IncreaseBaseStatByPercent(ref int percent, int addPercentValue, ref BigInteger currentValue, int baseStat)
    {
        percent += addPercentValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }

    // 임시
    BigInteger IncreaseBaseStatByPercent(ref int percent, int addPercentValue, ref BigInteger currentValue, float baseStat)
    {
        percent += addPercentValue;
        return CalculateTotal(((int)baseStat), percent, ref currentValue);
    }


    BigInteger DecreaseBaseStatByPercent(ref int percent, int subtractPercentValue, ref BigInteger currentValue, int baseStat)
    {
        percent -= subtractPercentValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }


    // 임시
    BigInteger DecreaseBaseStatByPercent(ref int percent, int subtractPercentValue, ref BigInteger currentValue, float baseStat)
    {
        percent -= subtractPercentValue;
        return CalculateTotal(((int)baseStat), percent, ref currentValue);
    }



    // 한번 봐야할 곳.
    BigInteger CalculateTotal(int baseStat, int percent, ref BigInteger currentValue)
    {
        // 백분율 증가 계산
        Debug.Log($"어디 한번 보자 :{baseStat} {percent} {BigInteger.Multiply(baseStat, percent)}");
        BigInteger percentIncrease = BigInteger.Multiply(baseStat, percent) / 100;
        //currentValue = BigInteger.Add(baseStat, percentIncrease);
        currentValue = baseStat + percentIncrease;

        Debug.Log("계산 결과: " + percentIncrease + "\n" + currentValue);
        return currentValue;
    }
}

