using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    Vector3 movementDirection;
    float lookDirection;




    void Start()
    {

    }

    void Update()
    {
        // PLAYER MOVEMENT
        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        //float xMov = Input.GetAxisRaw("Horizontal");
        //float zMov = Input.GetAxisRaw("Vertical");
        
        //movementDirection = new Vector3(xMov, 0, zMov).normalized * speed * Time.deltaTime;
        //movementDirection.Normalize();

        //transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }
        // ----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------






        // PLAYER ROTATION
        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        //float heading = Mathf.Atan2(Input.GetAxisRaw("Horizontal Look"), Input.GetAxisRaw("Vertical Look"));
        //if (Input.GetAxisRaw("Horizontal Look") + Input.GetAxisRaw("Vertical Look") != 0)
        //{
        //    transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);

        // ----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------



        

        // PLAYER ABILITY
        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------
        // ----------------------------------------------------------------------------------------
    }

    {
        //Player movement
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        //Player rotation
        if (lookDirection != 0)
        {
            transform.rotation = Quaternion.Euler(0f, lookDirection * Mathf.Rad2Deg, 0f);
        }
    }

    void OnMove(InputValue input)
    {
        Vector2 inputValue = input.Get<Vector2>();
        movementDirection = new Vector3(inputValue.x, 0, inputValue.y);
        movementDirection.Normalize();
    }

    void OnLook(InputValue input)
    {
        Vector2 inputValue = input.Get<Vector2>();
        lookDirection = Mathf.Atan2(inputValue.x, inputValue.y);
    }
}
