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
        if (PhotonNetwork.IsMasterClient)
        {
            gM = FindObjectOfType<GameMaster>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            print("Enemy has entered Endzone");
            Destroy(other.gameObject);
            gM.collectiveHealth -= 1;
        }
    }
}
