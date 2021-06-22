using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GrabAndThrow : MonoBehaviour
{
    public PhotonView pv;

    public MovementController movement;

    public GameObject ballPositon;

    public GameObject ballCommand;
    public GameObject crosshair;

    public Dodgeball db;

    public Vector3 myDirection;

    bool inRadius;
    bool grabbing;

    [SerializeField] float shootForce = 50f;
    Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        db = FindObjectOfType<Dodgeball>();
        movement = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (db.beingGrabbed == true && pv.IsMine && db.pv.IsMine && Input.GetButtonDown("Shoot"))
        {
            movement.anim.SetBool("Throw", true);
            pv.RPC("SendBallFlying", RpcTarget.AllBuffered);
        }
        else
        {
            movement.anim.SetBool("Throw", false);
        }

        if (inRadius == true)
        {
            ballCommand.SetActive(true);
        }
        else
        {
            ballCommand.SetActive(false);
        }

        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E) && !grabbing && !db.beingGrabbed && inRadius)
            {
                grabbing = true;
                movement.anim.SetBool("Pick Up", true);
                db.pv.RPC("GrabReleaseBall", RpcTarget.AllBuffered, true, ballPositon.transform.position);
                db.pv.RPC("EnableDisableRB", RpcTarget.MasterClient, true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && grabbing)
            {
                grabbing = false;
                movement.anim.SetBool("Pick Up", false);
                db.pv.RPC("GrabReleaseBall", RpcTarget.AllBuffered, false, Vector3.zero);
                db.pv.RPC("EnableDisableRB", RpcTarget.MasterClient, false);
                db.pv.RPC("ShootBall", RpcTarget.MasterClient, transform.forward * shootForce);
                
            }
            //else
            //{
            //    movement.anim.SetBool("Pick Up", false);
            //    movement.anim.SetBool("Drop", false);
            //}

            if (grabbing && ballPositon.transform.position != prevPos)
            {
                db.pv.RPC("BallGrabbed", RpcTarget.AllBuffered, ballPositon.transform.position);
                prevPos = ballPositon.transform.position;
            }
        }
    }

    [PunRPC]
    public void SendBallFlying()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.TryGetComponent(out Dodgeball d))
        {
            inRadius = true;
            d.grabScript = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.TryGetComponent(out Dodgeball d))
        {
            inRadius = false;
            d.grabScript = null;
        }
    }
}
