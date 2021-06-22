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

    public GameObject actualBall;
    //GameObject grabCommand;

    //Vector3 cameraPosition;
    //Camera playerCamera;

    [SerializeField]
    public GrabAndThrow grabScript;

    public bool inRadius;
    public bool beingGrabbed;

    [SerializeField] int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        grabScript = null;
        rb = GetComponentInParent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            Destroy(rb); //Only have master client apply rigidbody
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!grabScript.pv.IsMine) return;
        //if (inRadius == true)
        //{ 
        //    if (Input.GetKeyDown(KeyCode.E) && beingGrabbed == false) 
        //    {
        //        transform.position = grabScript.ballPositon.transform.position;
        //        beingGrabbed = true;
        //        if (PhotonNetwork.IsMasterClient)
        //        {
        //            rb.constraints = RigidbodyConstraints.FreezeAll;
        //        }
        //    }
        //    else if (Input.GetKeyDown(KeyCode.E) && beingGrabbed == true)
        //    {
        //        transform.position = actualBall.transform.position;
        //        beingGrabbed = false;
        //        if (PhotonNetwork.IsMasterClient)
        //        {
        //            rb.constraints = RigidbodyConstraints.None;
        //        }
        //    }
        //}

        //if (beingGrabbed)
        //{
        //    transform.position = grabScript.ballPositon.transform.position;
        //}
    }

    [PunRPC]
    public void EnableDisableRB(bool isDisabling)
    {
        if (isDisabling)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    [PunRPC]
    public void GrabReleaseBall(bool isGrabbing, Vector3 holdPos)
    {
        beingGrabbed = isGrabbing;
        if (isGrabbing)
        {
            transform.parent.position = holdPos;
        }
        else
        {
            transform.parent.position = actualBall.transform.position;
        }
    }

    [PunRPC]
    public void BallGrabbed(Vector3 holdPos)
    {
        transform.parent.position = holdPos;
    }

    [PunRPC]
    public void ShootBall(Vector3 force)
    {
        rb.AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            if(other.TryGetComponent(out DodgeballTarget target) && !beingGrabbed)
            {
                target.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, damage);
            }
        }
    }
}
