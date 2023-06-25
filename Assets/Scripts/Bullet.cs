using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        EnemyBehaviour enemyBehaviour = other.GetComponent<EnemyBehaviour>();
        var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

        switch (other.tag)
        {
            case "Enemy":
                enemyBehaviour.TakeHit(playerBehaviour.damage);
                Destroy(gameObject);
                break;

            default:
                Destroy(gameObject);
                break;
        }
    }
}
