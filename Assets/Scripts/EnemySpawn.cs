using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    string enemyTag = "Enemy";
    public int maxEnemies = 10;

    private Transform[] spawnPoints;

    void Start()
    {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        SpawnAtRandomPoint();
    }

    void SpawnAtRandomPoint()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

    int CurrentEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag(enemyTag).Length;
    }

    private void Update()
    {
        if (CurrentEnemyCount() < maxEnemies)
        {
            SpawnAtRandomPoint();
        }
    }
}

