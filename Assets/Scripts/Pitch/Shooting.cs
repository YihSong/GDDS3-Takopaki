using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKey("Fire1"))
    float timeElapsed;
    bool shooting;

    public float bulletForce = 20f;

    void Update()
    {
        if (shooting)
        {
            Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        }
    }

    void OnShoot ()
    {
        shooting = !shooting;

        //if (timeElapsed >= fireRate)
        //{
        //    Shoot();
        //    timeElapsed = 0;
        //}
    }
}
