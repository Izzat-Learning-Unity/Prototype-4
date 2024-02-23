using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private float speed = 6.0f;
    private Rigidbody enemyRB;
    private GameObject player;

    private float spawnRange = 9;
    public GameObject[] enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        InvokeRepeating("SpawnMinions", 2,4);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void SpawnMinions()
    {
        for (int i = 0; i < 2; i++)
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
