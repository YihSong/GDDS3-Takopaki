using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletController
{
    //public int fireBulletDamage;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("applyBurn");
            print("Burn Applied");
        }
    }

    IEnumerator applyBurn()
    {
        theEnemy.burn = true;
        yield return new WaitForSeconds(5);
        theEnemy.burn = false;
    }

}
