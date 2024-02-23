using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerB : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public GameObject[] powerUpPrefabs;
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber=1;

    public static GameObject[] spawnedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        GameObject powerup = Instantiate(powerUpPrefabs[0], GenerateSpawnPosition(), powerUpPrefabs[0].transform.rotation);
        powerup.name = "Powerup 1";

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber%5 == 0)//bossWave
            {
                SpawnBossWave();
            }
            else
            {
                SpawnEnemyWave(waveNumber);
                
            }

            int powerUpToSpawn = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerup = Instantiate(powerUpPrefabs[powerUpToSpawn], GenerateSpawnPosition(), powerUpPrefabs[powerUpToSpawn].transform.rotation);
            powerup.name = "Powerup " + (powerUpToSpawn + 1);
        }

        
    }

    private void SpawnBossWave()
    { 
        Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyToSpawnIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyToSpawnIndex], GenerateSpawnPosition(), enemyPrefabs[enemyToSpawnIndex].transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPos;
    }
}
