using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float enemyHP;
    public float maxHP = 5;
    public EnemyHealthbar healthbar;
    public GameObject currencyPrefab;

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
            DropCurrency();
            Destroy(gameObject);
            StatsManager.Instance.IncreaseKillCount();
        }
    }
}
