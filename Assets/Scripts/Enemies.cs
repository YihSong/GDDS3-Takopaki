using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Enemies : MonoBehaviour
{
    public GameObject goal;
    Animator animator;
    int health;
    public int startHealth;

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine) Destroy(GetComponent<Rigidbody>());
        //goal = FindObjectOfType<EndZone>().gameObject;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;
        health = startHealth;
        if (!photonView.IsMine) return;
        animator = GetComponent<Animator>();
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        if (photonView.IsMine)
        {
            animator.SetTrigger("Take Damage");
        }
        health -= _damage;
        Debug.Log(health);
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
            animator.SetTrigger("Die");
        }
        Destroy(gameObject);
    }
}
