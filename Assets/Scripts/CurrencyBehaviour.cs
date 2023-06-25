using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CurrencyBehaviour : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>(), true);
            StatsManager.Instance.IncreaseCurrency(UnityEngine.Random.Range(playerBehaviour.minCurrencyDrop, playerBehaviour.maxCurrencyDrop));
            Destroy(gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>(), false);
    }
}
