using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBullet : BulletController
{
    //public int electroBulletDamage;

    IEnumerator applyStatic()
    {
        theEnemy.electrocuted = true;
        if (theEnemy.burn)
        {
            theEnemy.ShowCombustionTrigger();
        }
        yield return new WaitForSeconds(5);
        theEnemy.electrocuted = false;
    }

    protected override void HitEnemy()
    {
        base.HitEnemy();
        StartCoroutine("applyStatic");
        print("Electrocuted Applied");
    }
}
