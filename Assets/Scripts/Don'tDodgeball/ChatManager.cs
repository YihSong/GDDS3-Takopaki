using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System;

public class ChatManager : MonoBehaviourPun,IPunObservable
{
    public GameObject chatTextPrefab;
    GameObject chatBackground;

    InputField ChatInput;
    [SerializeField]private bool DisableSend;

    MovementController mc;

    void Awake()
    {
        ChatInput = GameObject.Find("ChatInputField").GetComponent<InputField>();
        chatBackground = GameObject.Find("Chat Background");
    }

    void Start()
    {
        mc = GetComponent<MovementController>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (ChatInput.isFocused)
            {
                mc.disableInputs = true;
            }
            else
            {
                mc.disableInputs = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Pressing enter");
            }

            if (!DisableSend && ChatInput.isFocused)
            {
                Debug.Log("Ready to send");
                if (ChatInput.text != "" && ChatInput.text.Length > 1 && Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Trying to chat");
                    photonView.RPC("SendMsg", RpcTarget.AllBuffered, ChatInput.text);
                    ChatInput.text = "";    //Clear after sned
                    DisableSend = true;
                }
            }
        }
    }

    [PunRPC]
    void SendMsg(string msg)
    {
        GameObject ct = Instantiate(chatTextPrefab, Vector3.zero, Quaternion.identity, chatBackground.transform);
        ct.GetComponent<Text>().text = photonView.Owner.NickName + ": " + msg;
        StartCoroutine(hideBubbleSpeech(ct));
    }

    IEnumerator hideBubbleSpeech(GameObject _ct)
    {
        yield return new WaitForSeconds(3);
        Destroy(_ct);
        DisableSend = false;
    }

    // Send and receive date
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Local player can write
        }
        else if (stream.IsReading) {
        }
    }
}
