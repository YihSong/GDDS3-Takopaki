using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CatapultTower : DefaultTower
{
    Animator anim;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) return;
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("Fire", 0f, towerInfo.fireRate);
    }

    protected override void Fire()
    {
        if (target == null) return;
        anim.SetTrigger("Fire");
        base.Fire();
        
    }
}
