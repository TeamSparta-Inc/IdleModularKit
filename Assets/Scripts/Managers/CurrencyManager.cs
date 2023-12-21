using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Keiwando.BigInteger;
using TMPro;


public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    // 이벤트 : 통화의 양이 변경될 때 발생
    public event Action<string, string> OnCurrencyChanged;

    // 모든 통화의 목록 
    public List<Currency> currencies = new List<Currency>();


    private void Awake()
    {
        instance = this;
    }

    // 재화 매니저 초기화 메서드
    public void InitCurrencyManager()
    {
        OnCurrencyChanged += UpdateCurrencyUI;
        LoadCurrencies();
    }

    // 특정 통화를 증가시키는 메서드
    public void AddCurrency(string currencyName, BigInteger value)
    {
        Currency currency = currencies.Find(c => c.currencyName == currencyName);
        if (currency != null)
        {
            currency.Add(value);
            OnCurrencyChanged?.Invoke(currencyName, currency.amount); // 이벤트 발생
            SaveCurrencies();
        }
    }

    // 특정 통화를 감소시키는 메서드
    public bool SubtractCurrency(string currencyName, BigInteger value)
    {
        // 모든 통화중 매개변수로 받은 이름이 있나 체크
        Currency currency = currencies.Find(c => c.currencyName == currencyName);
        if (currency != null)
        {
            // 통화의 양을 감소시키, 결과에 따라 이벤트 발생
            bool result = currency.Subtract(value);
            SaveCurrencies();
            if (result)
            {
                OnCurrencyChanged?.Invoke(currencyName, currency.amount);
            }
            return result;
        }
        return false;
    }

    // 특정 통화의 현재 양을 반환하는 메서드
    public string GetCurrencyAmount(string currencyName)
    {
        Currency currency = currencies.Find(c => c.currencyName == currencyName);
        return currency?.amount ?? "0";
    }

    // 모든 통화를 로컬에 저장시키는 메서드
    public void SaveCurrencies()
    {
        ES3.Save<List<Currency>>("currencies", currencies);
    }

    // 로컬에 저장되어있는 모든 통화를 불러오는 메서드
    public bool LoadCurrencies()
    {
        if (ES3.KeyExists("currencies"))
        {
            currencies = ES3.Load<List<Currency>>("currencies");
            foreach (Currency currency in currencies)
            {
                OnCurrencyChanged?.Invoke(currency.currencyName, currency.amount); // 로딩 후 이벤트 발생
            }
        }
        else return false;
        return true;
    }

    // 통화의 UI를 업데이트 시키는 메서드
    void UpdateCurrencyUI(string currencyName, string amount)
    {
        Currency currency = currencies.Find(c => c.currencyName == currencyName);
        currency.currencyUI.text = BigInteger.ChangeMoney(amount);
    }
}
