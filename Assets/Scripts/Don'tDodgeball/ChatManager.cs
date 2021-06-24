using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System;

public class ChatManager : MonoBehaviourPun,IPunObservable
{
    public GameObject BubbleSpeech;
    public TextMeshProUGUI ChatText;

    InputField ChatInput;
    private bool DisableSend;

    void Awake()
    {
        ChatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            if (PhotonNetwork.IsMasterClient) //If we are the master client
            {
                ChatText.color = Color.blue; //Means we are on blue side
            }
            else
            {
                ChatText.color = Color.red; //We are not master client, we are on blue side
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient) //If we are the master client
            {
                ChatText.color = Color.red; //Means the other player is on red side
            }
            else
            {
                ChatText.color = Color.blue; //We are not master client, other player is on blue side
            }
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (ChatInput.isFocused)
            {

            }
            else
            {

            }

            if (!DisableSend && ChatInput.isFocused)
            {
                if (ChatInput.text != "" && ChatInput.text.Length > 1 && Input.GetKeyDown(KeyCode.Return))
                {
                    photonView.RPC("SendMsg", RpcTarget.AllBuffered, ChatInput.text);
                    BubbleSpeech.SetActive(true);
                    ChatInput.text = "";    //Clear after sned
                    DisableSend = true;
                }
            }
        }
    }

    [PunRPC]
    void SendMsg(string msg)
    {
        ChatText.text = msg;
        StartCoroutine(hideBubbleSpeech());
    }

    IEnumerator hideBubbleSpeech()
    {
        yield return new WaitForSeconds(3);
        BubbleSpeech.SetActive(false);
        DisableSend = false;
    }

    // Send and receive date
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Local player can write
            stream.SendNext(BubbleSpeech.activeSelf);
        }
        else if (stream.IsReading) {
            BubbleSpeech.SetActive((bool)stream.ReceiveNext());
        }
    }
}
