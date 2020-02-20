using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    float spawnRange = 9.5f;

    float minMassRange = 1f;
    float maxMassRange = 5f;

    int waves = 1;

    public GameObject powerUpPrefab;
    GameObject player;
    bool isGameOver = false;
    Vector3 initialPosition;
    GameObject canvas;
    public Text gameOverText;
    public Text wavesCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waves);
        Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);

        player = GameObject.Find("Player");
        initialPosition = player.transform.position;
        canvas = GameObject.Find("Canvas");
        gameOverText = canvas.transform.GetChild(0).GetComponent<Text>();
        gameOverText.enabled = false;

        int powerupCounter = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if (powerupCounter == 0)
        {
            Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        }
        else if (powerupCounter > 1)
        {
            for(int i = 0; i < powerupCounter; i++)
            {
                GameObject PowerupToDestroy = GameObject.FindGameObjectWithTag("Powerup");
                Destroy(PowerupToDestroy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesCount = FindObjectsOfType<EnemyBehavior>().Length;
        if (enemiesCount == 0 && !isGameOver)
        {
            waves++;
            spawnEnemyWave(waves);
            Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        }

        if (player.transform.position.y <= -10)
        {
            isGameOver = true;
            gameOverText.enabled = true;
            StartCoroutine(GameOverTimer());
        }

    }

    IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(5);
        RestarGame();
        StopCoroutine(GameOverTimer());

    }
    void RestarGame()
    {
        player.transform.position = initialPosition;
        waves = 1;
        spawnEnemyWave(waves);
        Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        isGameOver = false;
        gameOverText.enabled = false;
        
    }

    Vector3 GenerateRandomPosition()
    {
        float spawnRandomX = Random.Range(-spawnRange, spawnRange);
        float spawnRandomZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnRandomPosition = new Vector3(spawnRandomX, 0, spawnRandomZ);
        //Debug.Log("Spreading!" + spawnRandomPosition);
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

            Debug.Log("Wave " + waves); 
        }
    }

}
