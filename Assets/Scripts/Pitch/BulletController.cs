using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;

    public EnemyController theEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            theEnemy = other.GetComponent<EnemyController>();
            HitEnemy();
        }
        print("hit" + other.name + "!");
        //Destroy(gameObject);
    }

    protected virtual void HitEnemy()
    {

    }
}
