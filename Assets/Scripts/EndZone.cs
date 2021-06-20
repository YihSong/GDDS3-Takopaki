using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class EndZone : MonoBehaviour
{
    public GameMaster gM;
    PhotonView pv;


    // Start is called before the first frame update
    void Start()
    {
        //if (!PhotonNetwork.IsMasterClient) Destroy(gameObject);
        gM = FindObjectOfType<GameMaster>();
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            pv.RPC("PlayerDamage", RpcTarget.All);
            Destroy(other.gameObject);
        }
    }

    [PunRPC]
    public void PlayerDamage()
    {
        print("Enemy has entered Endzone");
        gM.collectiveHealth -= 1;
    }
}
