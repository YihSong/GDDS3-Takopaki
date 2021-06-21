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
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }

        if (db.beingGrabbed == true && pv.IsMine && db.pv.IsMine && Input.GetKeyDown("Shoot"))
        {
            pv.RPC("SendBallFlying", RpcTarget.AllBuffered);
        }

        if (db.inRadius == true)
        {
            ballCommand.SetActive(true);
        }
        else
        {
            ballCommand.SetActive(false);
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
            d.inRadius = true;
            d.grabScript = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.TryGetComponent(out Dodgeball d))
        {
            d.inRadius = false;
            d.grabScript = null;
        }
    }
}
