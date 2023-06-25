using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UpgradeSystem : MonoBehaviour
{
    public Sprite upgradedSprite;

    private Dictionary<Button, (List<Image>, int, System.Action)> buttonToUpgrades;
    private int[] upgradePrices = { 500, 1000, 2000, 5000 };
    private int playerCurrency;

    void Start()
    {
        buttonToUpgrades = new Dictionary<Button, (List<Image>, int, System.Action)>();

        playerCurrency = StatsManager.Instance.currencyAmount;

        int i = 0;
        foreach (Transform upgradeSystem in transform)
        {
            Button button = upgradeSystem.GetComponentInChildren<Button>();
            List<Image> slots = new List<Image>();
            foreach (Transform child in upgradeSystem)
            {
                Image image = child.GetComponent<Image>();
                if (image != null && image != button.image)
                {
                    slots.Add(image);
                }
            }

            System.Action upgradeAction = GetUpgradeAction(i);
            buttonToUpgrades[button] = (slots, 0, upgradeAction);
            button.onClick.AddListener(() => Upgrade(button));
            i++;
        }
    }

    private void Update()
    {
        playerCurrency = StatsManager.Instance.currencyAmount;
    }

    void Upgrade(Button button)
    {
        (List<Image> slots, int currentIndex, System.Action upgradeAction) = buttonToUpgrades[button];
        if (currentIndex < slots.Count && playerCurrency >= upgradePrices[currentIndex])
        {
            slots[currentIndex].sprite = upgradedSprite;
            playerCurrency -= upgradePrices[currentIndex];
            StatsManager.Instance.DecreaseCurrency(upgradePrices[currentIndex]);
            buttonToUpgrades[button] = (slots, currentIndex + 1, upgradeAction);
            upgradeAction.Invoke();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    System.Action GetUpgradeAction(int i)
    {
        switch (i)
        {
            case 0:
                return IncreaseMovementSpeed;
            case 1:
                return IncreaseFireRate;
            case 2:
                return IncreaseMaxAmmo;
            case 3:
                return IncreaseBulletDamage;
            case 4:
                return IncreaseReloadSpeed;
            case 5:
                return IncreaseCurrencyDrop;
            default:
                return null;
        }
    }

    void IncreaseMovementSpeed()
    {
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.movementSpeed += 1f;
    }

    void IncreaseFireRate()
    {
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.fireRate += 1.0f;
    }

    void IncreaseMaxAmmo()
    {
        StatsManager.Instance.maxAmmo += 10;
    }

    void IncreaseBulletDamage()
    {
        var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerBehaviour.damage += 1;
    }

    void IncreaseReloadSpeed()
    {
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.reloadRate += 1.0f;
    }

    void IncreaseCurrencyDrop()
    {
        var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerBehaviour.maxCurrencyDrop += 50;
        playerBehaviour.minCurrencyDrop += 50;
    }
}
