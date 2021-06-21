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
                db.transform.position = ballPositon.transform.position;
                grabbing = true;
                db.beingGrabbed = true;
                db.pv.RPC("EnableDisableRB", RpcTarget.MasterClient, true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && grabbing)
            {
                db.transform.position = db.actualBall.transform.position;
                grabbing = false;
                db.beingGrabbed = false;
                db.pv.RPC("EnableDisableRB", RpcTarget.MasterClient, false);
            }

            if (grabbing)
            {
                db.transform.position = ballPositon.transform.position;
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
