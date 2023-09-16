using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public class EnemyBehaviour : MonoBehaviour
{
    public float enemyHP;
    public float maxHP = 5;
    public EnemyHealthbar healthbar;
    public GameObject currencyPrefab;

    public AIPath ai;

    void Start()
    {
        enemyHP = maxHP;
        healthbar.EnemySetHealth(enemyHP, maxHP);
    }

    public void TakeHit(float damage)
    {
        enemyHP -= damage;
    }

    public void DropCurrency()
    {
        GameObject currency = Instantiate(currencyPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        healthbar.EnemySetHealth(enemyHP, maxHP);

        if (enemyHP <= 0)
        {
            switch (gameObject.name)
            {
                case "Boss Enemy(Clone)":
                    StatsManager.Instance.IncreaseCurrency(StatsManager.Instance.currencyAmount);
                    break;
                default:
                    DropCurrency();
                    break;
            }
            Destroy(gameObject);
            StatsManager.Instance.IncreaseKillCount();
        }
    }

    void FixedUpdate()
    {
        if(ai.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (ai.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
