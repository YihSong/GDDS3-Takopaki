using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;


    public void Start()
    {
        InvokeRepeating("Spawn", 1f, 2f);
    }
    public void Update()
    {

    }

    public void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
    }
}
