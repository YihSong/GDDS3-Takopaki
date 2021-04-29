using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{

    public float dashSpeed;
    Rigidbody rb;
    public bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (Input.GetButtonDown("Dash"))
        //{
        //    isDashing = true;
        //}
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dashed();
        }
    }

    private void Dashed()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = false;

        
    }


    void OnDash()
    {
        isDashing = true;
    }
}
