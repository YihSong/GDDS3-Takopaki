﻿using System.Collections;
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
        if (pv.IsMine)
        {
            gM = FindObjectOfType<GameMaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [PunRPC]
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            gM.collectiveHealth -= 1;
        }
    }
}
