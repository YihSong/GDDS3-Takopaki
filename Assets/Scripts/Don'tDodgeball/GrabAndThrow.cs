using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GrabAndThrow : MonoBehaviour
{
    public PhotonView pv;

    public GameObject ballPositon;

    public GameObject ballCommand;
    public GameObject crosshair;

    public Dodgeball db;

    public Vector3 myDirection;

    [SerializeField]bool inRadius;
    bool grabbing;

    

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        db = FindObjectOfType<Dodgeball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (db.beingGrabbed == true && db.pv.IsMine && pv.IsMine)
        {
            //crosshair.gameObject.SetActive(true);
        }
        else
        {
            //crosshair.gameObject.SetActive(false);
        }

        if (db.beingGrabbed == true && pv.IsMine && db.pv.IsMine && Input.GetButtonDown("Shoot"))
        {
            pv.RPC("SendBallFlying", RpcTarget.AllBuffered);
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
                db.pv.RPC("GrabReleaseBall", RpcTarget.AllBuffered, false, ballPositon.transform.position);
                db.pv.RPC("EnableDisableRB", RpcTarget.MasterClient, true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && grabbing)
            {
                grabbing = false;
                db.pv.RPC("GrabReleaseBall", RpcTarget.AllBuffered, false, Vector3.zero);
                db.pv.RPC("EnableDisableRB", RpcTarget.MasterClient, false);
            }

            if (grabbing)
            {
                db.pv.RPC("BallGrabbed", RpcTarget.AllBuffered, ballPositon.transform.position);
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
