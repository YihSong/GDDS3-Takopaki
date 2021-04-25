using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] enemies;

    int randomSpawnPoint, randomEnemy;
    public static bool spawnAllowed;

    public int EnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnAnEnemy", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAnEnemy()
    {
        if (spawnAllowed && EnemyCount > 0)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            EnemyCount -= 1;
        }
    }
}
