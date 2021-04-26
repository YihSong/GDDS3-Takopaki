using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        print("hit" + other.name + "!");
        //Destroy(gameObject);
    }

}
