using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    public Slider slider;
    public Color lowHP;
    public Color highHP;

    public void PlayerSetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(true);
        slider.maxValue = maxHealth;
        slider.value = health;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowHP, highHP, slider.normalizedValue);
    }
}
