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

        // ----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------

        // PLAYER ROTATION
        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        float heading = Mathf.Atan2(Input.GetAxisRaw("Horizontal Look"), Input.GetAxisRaw("Vertical Look"));
        if (Input.GetAxisRaw("Horizontal Look") + Input.GetAxisRaw("Vertical Look") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
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
