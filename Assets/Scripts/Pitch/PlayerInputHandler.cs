using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{

    PlayerInput playerInput;
    PlayerController playerController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Debug.Log("Looking for player " + playerInput.playerIndex);
        foreach (PlayerController controller in FindObjectsOfType<PlayerController>())
        {
            Debug.Log("Found player " + controller.index);
            if(controller.index == playerInput.playerIndex)
            {
                playerController = controller;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue input)
    {
        Vector2 controllerInput = input.Get<Vector2>();

        playerController.movementDirection = new Vector3(controllerInput.x, 0, controllerInput.y);
    }

    public void OnLook(InputValue input)
    {
        Vector2 controllerInput = input.Get<Vector2>();

        if (controllerInput.x + controllerInput.y != 0)
        {
            playerController.RotatePlayer(Mathf.Atan2(controllerInput.x, controllerInput.y));
        }
    }

    void OnShoot()
    {
        playerController.shooting = !playerController.shooting;
    }
}
