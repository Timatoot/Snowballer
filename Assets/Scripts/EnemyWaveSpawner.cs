using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numEnemiesSpawned;
    public GameObject[] enemyPrefab;
    public float spawnInterval;
}

public class EnemyWaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Animator animator;

    private Wave currentWave;
    public TMPro.TextMeshProUGUI nextWaveNumberDisplay;
    public TMPro.TextMeshProUGUI currWaveNumberDisplay;
    private int currentWaveNumber;
    private int waveType;
    private int maxEnemiesInWave;

    private bool allowedToSpawn = true;
    private bool allowedToAnimate = false;
    private float nextSpawnTime;

    private void Start()
    {
        currentWave = waves[waveType];
        currentWaveNumber = 1;
        string childName = "Spawnpoints";

        Transform specificChild = transform.Find(childName);

        spawnPoints = new Transform[specificChild.childCount];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = specificChild.GetChild(i);
        }

        maxEnemiesInWave = currentWave.numEnemiesSpawned;
        currWaveNumberDisplay.text = "Wave: " + currentWaveNumber.ToString();
    }

    void Update()
    {
        CheckWaveType();
        currentWave = waves[waveType];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length <= 0 && allowedToAnimate)
        {
            currWaveNumberDisplay.text = "Wave: " + currentWaveNumber.ToString();
            int nextWave = currentWaveNumber + 1;
            nextWaveNumberDisplay.text = "Wave: " + nextWave.ToString();
            animator.SetTrigger("Wave Complete");
            allowedToAnimate = false;
            CheckWaveType();
            ResetEnemyCounts();
        }
    }

    void CheckWaveType()
    {
        if (currentWaveNumber % 10 == 0)
        {
            waveType = 2;
        }
        else if (currentWaveNumber % 5 == 0)
        {
            waveType = 1;
        }
        else
        {
            waveType = 0;
        }
    }

    void ResetEnemyCounts()
    {
        currentWave.numEnemiesSpawned = maxEnemiesInWave;
        currentWave = waves[waveType];
        maxEnemiesInWave = currentWave.numEnemiesSpawned;
    }

    void SpawnNextWave()
    {
        allowedToSpawn = true;
        currentWaveNumber++;
    }

    void SpawnWave()
    {
        if (allowedToSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.enemyPrefab[Random.Range(0, currentWave.enemyPrefab.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.numEnemiesSpawned--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.numEnemiesSpawned <= 0)
            {
                allowedToSpawn = false;
                allowedToAnimate = true;
            }
        }
    }
}

