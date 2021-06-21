using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Dodgeball : MonoBehaviour
{
    public PhotonView pv;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    GameObject actualBall;
    GameObject grabCommand;

    [SerializeField]
    Vector3 cameraPosition;
    Camera playerCamera;

    [SerializeField]
    GrabAndThrow grabScript;

    public bool inRadius;
    public bool beingGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        grabScript = FindObjectOfType<GrabAndThrow>();
        rb = FindObjectOfType<Rigidbody>();
        pv = FindObjectOfType<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabScript.pv.IsMine) return;
        if (inRadius == true)
        {
            // Get Command on the ball to constantly be looking at the player cameras;
            cameraPosition = playerCamera.transform.position;
            Transform commandPosition = grabCommand.transform;
            commandPosition.LookAt(cameraPosition);

            if (Input.GetKeyDown("Grab") && beingGrabbed == false) 
            {
                actualBall.transform.position = grabScript.ballPositon.transform.position;
                beingGrabbed = true;
            }
            else if (Input.GetKeyDown("Grab") && beingGrabbed == true)
            {
                actualBall.transform.position = actualBall.transform.position;
                beingGrabbed = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRadius = true;
        }
        else
        {
            inRadius = false;
        }
    }
}
