using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DodgeballTarget : MonoBehaviour
{
    [SerializeField] int startHealth;
    int health;
    [SerializeField] Image healthBar;

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        healthBar.fillAmount = health / startHealth;
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
