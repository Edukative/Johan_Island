using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    float spawnRange = 10;

    float minMassRange = 1f;
    float maxMassRange = 5f;

    int waves = 1;

    public GameObject powerUpPrefab;
    GameObject player;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waves);
        Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);

        player = GameObject.Find("Player");
        initialPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesCount = FindObjectsOfType<EnemyBehavior>().Length;
        if (enemiesCount == 0)
        {
            waves++;
            spawnEnemyWave(waves);
            Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        }

        if (player.transform.position.y == -20)
        {
            Destroy(player.gameObject);
            Debug.Log("ur kil");
        }

    }

    Vector3 GenerateRandomPosition()
    {
        float spawnRandomX = Random.Range(-spawnRange, spawnRange);
        float spawnRandomZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnRandomPosition = new Vector3(spawnRandomX, 0, spawnRandomZ);
        Debug.Log("Spreading!" + spawnRandomPosition);
        return spawnRandomPosition;
    }

    void spawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
            Rigidbody enemyRB = enemy.GetComponent<Rigidbody>();
            EnemyBehavior enemyScript = enemy.GetComponent<EnemyBehavior>();

            enemyRB.mass = Random.Range(minMassRange, maxMassRange);
            float currentMass = enemyRB.mass;
            enemy.transform.localScale = new Vector3(currentMass, currentMass, currentMass);
        }
    }
}
