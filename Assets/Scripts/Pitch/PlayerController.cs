using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public Vector3 movementDirection;

    public int index;

    public bool electrocuted;
    public bool burn;
    public bool wet;
    public bool grounded;

    void Start()
    {

    }

    void Update()
    {

    }


    private void FixedUpdate()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        //if (heading != 0)
        //{
        //    transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
        //}

    }

    //public void OnMove (InputValue input)
    //{
    //    Vector2 controllerInput = input.Get<Vector2>();

    //    movementDirection = new Vector3(controllerInput.x, 0, controllerInput.y);
    //}

    //public void OnLook (InputValue input)
    //{
    //    Vector2 controllerInput = input.Get<Vector2>();

    //    heading = Mathf.Atan2(controllerInput.x, controllerInput.y);
    //    if (controllerInput.x + controllerInput.y != 0)
    //    {
    //        transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
    //        Debug.Log(transform.rotation.y);
    //    }
    //}

    public void RotatePlayer(float angle)
    {
        transform.rotation = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);
    }
}
