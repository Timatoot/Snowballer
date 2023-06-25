using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public TextMeshProUGUI eliminationsText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI currencyText;
    private int kills = 0;
    public int ammo = 20;
    public int maxAmmo = 20;
    public int currencyAmount = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        UpdateKillText();
        AmmoCount();
        UpdateCurrencyText();
    }

    void Update()
    {
        AmmoCount();
        UpdateCurrencyText();
    }

    public void IncreaseKillCount()
    {
        kills++;
        UpdateKillText();
    }

    public void IncreaseCurrency(int amount)
    {
        currencyAmount += amount;
    }

    public void DecreaseCurrency(int amount)
    {
        currencyAmount -= amount;
    }

    public void DecreaseAmmoCount()
    {
        ammo--;
        AmmoCount();
    }

    public void IncreaseAmmoCount()
    {
        ammo++;
        AmmoCount();
    }

    private void UpdateKillText()
    {
        eliminationsText.text = "Enemies Eliminated: " + kills.ToString();
    }

    public void AmmoCount()
    {
        ammoText.text = ammo.ToString();
    }

    public void UpdateCurrencyText()
    {
        currencyText.text = currencyAmount.ToString();
    }
}
