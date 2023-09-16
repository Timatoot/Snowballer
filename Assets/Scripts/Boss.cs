using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject bossAttackPrefab;
    public float minSpawnTime = 4f;
    public float maxSpawnTime = 8f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(SpawnAttack());
    }

    IEnumerator SpawnAttack()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(delay);

            Instantiate(bossAttackPrefab, player.transform.position, Quaternion.identity);
        }
    }
}
