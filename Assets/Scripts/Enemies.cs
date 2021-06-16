using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Enemies : MonoBehaviour
{
    public GameObject goal;
    int health;
    public int startHealth;

    // Start is called before the first frame update
    void Start()
    {
        goal = FindObjectOfType<EndZone>().gameObject;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;
        health = startHealth;
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        Debug.Log(health);
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
