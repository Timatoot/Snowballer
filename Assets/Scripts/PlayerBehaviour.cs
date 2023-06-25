using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static event Action onPlayerDeath;
    public float playerHP;
    public float maxHP = 10;
    public PlayerHealthbar healthbar;
    public int minCurrencyDrop = 100;
    public int maxCurrencyDrop = 200;
    public int damage = 1;

    void Start()
    {
        playerHP = maxHP;
        healthbar.PlayerSetHealth(playerHP, maxHP);
    }

    public float CheckHealth()
    {
        return playerHP;
    }

    public void TakeHit(float damage)
    {
        playerHP -= damage;

        healthbar.PlayerSetHealth(playerHP, maxHP);

        if (CheckHealth() <= 0)
        {
            onPlayerDeath?.Invoke();
            CursorChanger cursorChanger = FindObjectOfType<CursorChanger>();
            cursorChanger.SetMenuCursor();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        switch (other.tag)
        {
            case "Enemy":
                TakeHit(1);
                break;

            default:
                break;
        }
    }
}
