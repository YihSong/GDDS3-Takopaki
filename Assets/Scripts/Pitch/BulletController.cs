using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;

    public EnemyController theEnemy;
    protected virtual void OnTriggerEnter(Collider other)
    {
        print("hit" + other.name + "!");
        //Destroy(gameObject);
    }

}
