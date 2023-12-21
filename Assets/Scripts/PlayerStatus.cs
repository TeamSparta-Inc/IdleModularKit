using System.Collections.Generic;
using UnityEngine;
using Keiwando.BigInteger;
using System;


[Serializable]
public class PlayerStatus : BaseStatus
{
    [SerializeField]
    BigInteger attackPercent = 0;
    BigInteger currentAttackValue = 0;

    [SerializeField]
    BigInteger healthPercent = 0;
    BigInteger currentHealthValue = 0;

    [SerializeField]
    BigInteger defensePercent = 0;
    BigInteger currentDefenseValue = 0;

    [SerializeField]
    BigInteger critChancePercent = 0;
    BigInteger currentCritChanceValue = 0;

    [SerializeField]
    BigInteger critDamagePercent = 0;
    BigInteger currentCritDamageValue = 0;


    // 스텟 증가 메서드
    public void IncreaseBaseStat(StatusType statusType, int addValue)
    {
        switch (statusType)
        {
            case StatusType.ATK:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStat(ref baseAttack, addValue, ref currentAttackValue, attackPercent));
                break;
            case StatusType.HP:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStat(ref baseHealth, addValue, ref currentHealthValue, healthPercent));
                break;
            case StatusType.DEF:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStat(ref baseDefense, addValue, ref currentDefenseValue, defensePercent));
                break;
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStat(ref baseCritDamage, addValue, ref currentCritDamageValue, critDamagePercent));
                break;
        }
        return;
    }
    public void IncreaseBaseStat(StatusType statusType, float addValue)
    {
        switch (statusType)
        {
            case StatusType.CRIT_CH:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStat(ref baseCritChance, addValue, ref currentCritChanceValue, critChancePercent));
                break;
        }
        return;
    }

    // 스텟 감소 메서드
    public void DecreaseBaseStat(StatusType statusType, int subtractValue)
    {
        Debug.Log("누구냐 넌 : " + statusType);
        switch (statusType)
        {
            case StatusType.ATK:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStat(ref baseAttack, subtractValue, ref currentAttackValue, attackPercent));
                break;
            case StatusType.HP:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStat(ref baseHealth, subtractValue, ref currentHealthValue, healthPercent));
                break;
            case StatusType.DEF:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStat(ref baseDefense, subtractValue, ref currentDefenseValue, defensePercent));
                break;
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStat(ref baseCritDamage, subtractValue, ref currentCritDamageValue, critDamagePercent));
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
        }
        return;
    }

    // 스텟 퍼센트 증가 메서드
    public void IncreaseBaseStatByPercent(StatusType statusType, BigInteger addPercent)
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
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statusType, IncreaseBaseStatByPercent(ref critDamagePercent, addPercent, ref currentCritDamageValue, baseCritDamage));
                break;
        }
    }

    // 스텟 퍼센트 감소 메서드
    public void DecreaseBaseStatByPercent(StatusType statusType, BigInteger subtractPercent)
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
            case StatusType.CRIT_DMG:
                Player.instance.SetCurrentStatus(statusType, DecreaseBaseStatByPercent(ref critDamagePercent, subtractPercent, ref currentCritDamageValue, baseCritDamage));
                break;
        }
    }

    // 스텟 증가 연산 메서드
    BigInteger IncreaseBaseStat(ref int baseStat, int addValue, ref BigInteger currentValue, BigInteger percent)
    {
        baseStat += addValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }
    BigInteger IncreaseBaseStat(ref float baseStat, float addValue, ref BigInteger currentValue, BigInteger percent)
    {
        baseStat += addValue;
        return CalculateTotal(((int)baseStat), percent, ref currentValue);
    }

    // 스텟 감소 연산 메서드
    BigInteger DecreaseBaseStat(ref int baseStat, int subtractValue, ref BigInteger currentValue, BigInteger percent)
    {
        baseStat -= subtractValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }
    BigInteger DecreaseBaseStat(ref float baseStat, float subtractValue, ref BigInteger currentValue, BigInteger percent)
    {
        baseStat -= subtractValue;
        return CalculateTotal(((int)baseStat), percent, ref currentValue);
    }

    // 스텟 퍼센트 증가 연산 메서드
    BigInteger IncreaseBaseStatByPercent(ref BigInteger percent, BigInteger addPercentValue, ref BigInteger currentValue, int baseStat)
    {
        percent += addPercentValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }

    // 스텟 퍼센트 감소 연산 메서드
    BigInteger DecreaseBaseStatByPercent(ref BigInteger percent, BigInteger subtractPercentValue, ref BigInteger currentValue, int baseStat)
    {
        percent -= subtractPercentValue;
        return CalculateTotal(baseStat, percent, ref currentValue);
    }

    // 총 공격력 합산 메서드
    BigInteger CalculateTotal(int baseStat, BigInteger percent, ref BigInteger currentValue)
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

