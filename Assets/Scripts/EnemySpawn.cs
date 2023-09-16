using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject innerCircle;
    public GameObject outerCircle;
    string enemyTag = "Enemy";
    public int maxEnemies = 5;

    private CircleCollider2D innerCircleCollider;
    private CircleCollider2D outerCircleCollider;

    private void Start()
    {
        innerCircleCollider = innerCircle.GetComponent<CircleCollider2D>();
        outerCircleCollider = outerCircle.GetComponent<CircleCollider2D>();

        innerCircleCollider.enabled = false;
        outerCircleCollider.enabled = false;

        setTransparent(innerCircle.GetComponent<SpriteRenderer>());
        setTransparent(outerCircle.GetComponent<SpriteRenderer>());

        StartCoroutine(SpawnGoons(2.0f));
    }

    private void setTransparent(SpriteRenderer obj)
    {
        SpriteRenderer renderer = obj;
        Color color = renderer.color;
        color.a = 0f;
        renderer.color = color;
    }

    void SpawnAtPoint()
    {
        float innerRadius = innerCircleCollider.radius * innerCircle.transform.lossyScale.x;
        float outerRadius = outerCircleCollider.radius * outerCircle.transform.lossyScale.x;
        Vector3 center = innerCircle.transform.position;

        float spawnRadius = UnityEngine.Random.Range(innerRadius, outerRadius);
        float spawnAngle = UnityEngine.Random.Range(0, 2 * Mathf.PI);

        Vector3 spawnDirection = new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0);
        Vector3 spawnPoint = center + spawnDirection * spawnRadius;

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    int CurrentEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag(enemyTag).Length - 1;
    }
        
    IEnumerator SpawnGoons(float delay)
    {
        while (true) 
        {  
            yield return new WaitForSeconds(delay);

            if (CurrentEnemyCount() < maxEnemies)
            {
                SpawnAtPoint();
            }
        }
    }
}

