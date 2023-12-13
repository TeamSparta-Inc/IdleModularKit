using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keiwando.BigInteger;
using TMPro;

[Serializable]
public class Currency
{
    public string currencyName;
    public string amount;
    public TMP_Text currencyUI;


    public void Add(BigInteger value)
    {
        BigInteger currentAmount = new BigInteger(amount);
        currentAmount += value;
        amount = currentAmount.ToString();
    }

    public bool Subtract(BigInteger value)
    {
        BigInteger currentAmount = new BigInteger(amount);
        if (currentAmount - value < 0) return false;
        currentAmount -= value;
        amount = currentAmount.ToString();
        return true;
    }


    public Currency(string currencyName, string initialAmount)
    {
        this.currencyName = currencyName;
        this.amount = initialAmount;
    }
}
