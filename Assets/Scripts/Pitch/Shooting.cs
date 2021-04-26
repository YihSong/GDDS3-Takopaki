using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        Destroy(bullet, 2);
    }
}
