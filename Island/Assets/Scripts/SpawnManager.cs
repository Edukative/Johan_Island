using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    float spawnRange = 10;
    // Start is called before the first frame update
    void Start()
    {

        Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    Vector3 GenerateRandomPosition()
    {
        float spawnRandomX = Random.Range(-spawnRange, spawnRange);
        float spawnRandomZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnRandomPosition = new Vector3(spawnRandomX, 0, spawnRandomZ);
        Debug.Log("The virus is multiplicating!" + spawnRandomPosition);
    }
}
