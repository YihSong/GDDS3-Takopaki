using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float fireRate;
    public Transform firePoint;
    public GameObject bulletPrefab;
    float timeElapsed;

    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (timeElapsed >= fireRate)
            {
                Shoot();
                timeElapsed = 0;
            }
        }
        timeElapsed += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        Destroy(bullet, 2);
    }
}
