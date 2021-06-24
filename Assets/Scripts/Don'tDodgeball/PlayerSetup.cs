using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject FPSCamera;

    [SerializeField]
    TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {

        if (photonView.IsMine)
        {
            transform.GetComponent<MovementController>().enabled = true;
            FPSCamera.GetComponent<Camera>().enabled = true;
            FPSCamera.GetComponent<AudioListener>().enabled = true;
            if (PhotonNetwork.IsMasterClient) //If we are the master client
            {
                playerNameText.color = Color.blue; //Means we are on blue side
            }
            else
            {
                playerNameText.color = Color.red; //We are not master client, we are on blue side
            }
        }
        else
        {
            transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false;
            FPSCamera.GetComponent<AudioListener>().enabled = false;
            if (PhotonNetwork.IsMasterClient) //If we are the master client
            {
                playerNameText.color = Color.red; //Means the other player is on red side
            }
            else
            {
                playerNameText.color = Color.blue; //We are not master client, other player is on blue side
            }
        }


        SetPlayerUI();

    }

    public void SetPlayerUI()
    {
        if (playerNameText!=null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }
    }

    
}
