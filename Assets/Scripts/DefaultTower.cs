using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DefaultTower : MonoBehaviour
{
    public Transform target;
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem shootFX;

    public Transform partToRotate;

    [SerializeField] protected TowerInfo towerInfo;

    protected PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        InvokeRepeating("FireFX", 0f, towerInfo.fireRate);
        if (!pv.IsMine) return;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("Fire", 0f, towerInfo.fireRate);
    }

    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(towerInfo.enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Found enemy in our range
        if (nearestEnemy != null && shortestDistance <= towerInfo.range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (!pv.IsMine) return;
        if (target == null)
            return;

        // Tracking the enemy 
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * towerInfo.turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
    }

    protected virtual void Fire()
    {
        if (target == null) return;
        Debug.Log("Shooting");
        RaycastHit hit;
        if(Physics.Raycast(firePoint.position, target.position - firePoint.position, out hit, towerInfo.range, towerInfo.layerMask))
        {
            Debug.DrawRay(firePoint.position, target.position - firePoint.position * towerInfo.range, Color.white);
            Debug.Log("Hit Enemy");
            hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, towerInfo.damage);
        }
    }

    void FireFX()
    {
        shootFX.Play();
    }
}
