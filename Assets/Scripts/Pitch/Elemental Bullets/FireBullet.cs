using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletController
{
    //public int fireBulletDamage;

    IEnumerator applyBurn()
    {
        theEnemy.burn = true;
        yield return new WaitForSeconds(5);
        theEnemy.burn = false;
    }

    protected override void HitEnemy()
    {
        base.HitEnemy();
        StartCoroutine("applyBurn");
        print("Burn Applied");
    }
}
