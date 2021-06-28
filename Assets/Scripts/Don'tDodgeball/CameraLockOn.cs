using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    public bool lockOn;
    Dodgeball dodgeball;
    // Start is called before the first frame update
    void Start()
    {
        dodgeball = FindObjectOfType<Dodgeball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockOn)
        {
            Vector3 dir = dodgeball.transform.position - transform.position;
            dir.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;
        }
    }
}
