using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    bool shooting;
    float timeElapsed;
    public float fireRate;
    public float bulletSpeed;

    void Update()
    {
        //if (Input.GetKey("Fire1"))
        //{
        //    Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        //}
        if (shooting)
        {
            if(timeElapsed >= fireRate)
            {
                Shoot();
                timeElapsed = 0;
            }
        }
        timeElapsed += Time.deltaTime;
    }

    void OnShoot()
    {
        shooting = !shooting;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(bullet, 1f);
    }
}
