using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBullet : BulletController
{
    //public int electroBulletDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("applyStatic");
            print("Electrocuted Applied");
        }
    }

    IEnumerator applyStatic()
    {
        theEnemy.electrocuted = true;
        yield return new WaitForSeconds(5);
        theEnemy.electrocuted = false;
    }
}
