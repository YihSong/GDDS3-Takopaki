using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;




    void Start()
    {

    }

    void Update()
    {
        // PLAYER MOVEMENT
        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        
        Vector3 movementDirection = new Vector3(xMov, 0, zMov).normalized * speed * Time.deltaTime;
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        // ----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------



        

        // PLAYER ABILITY
        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------
    }

}
