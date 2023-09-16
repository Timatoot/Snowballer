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

    Rigidbody2D rb;

    void Start()
    {
        enemyHP = maxHP;
        healthbar.EnemySetHealth(enemyHP, maxHP);

        rb = GetComponent<Rigidbody2D>();
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
    //TODO: rb not set to an object referenece
    void FixedUpdate()
    {
        if (rb.velocity.x >= 0.25f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.25f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
