using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class EndScreenManager : MonoBehaviourPunCallbacks
{
    public Animator bluePlayer, redPlayer;
    public Text winText;
    public TextMeshProUGUI blueName, redName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndScreen(string _blueName, string _redName, bool redWon)
    {
        blueName.text = _blueName;
        redName.text = _redName;
        bluePlayer.SetBool("Winner", !redWon);
        redPlayer.SetBool("Winner", redWon);
        if (redWon)
        {
            winText.text = _redName + " WON";
            winText.color = Color.red;
            if (PhotonNetwork.IsMasterClient) //If we are blue
            {
                AudioManager.instance.Play("Lose" + Random.Range(1, 3));
            }
            else
            {
                AudioManager.instance.Play("Win" + Random.Range(1, 4));
            }
        }
        else
        {
            winText.text = _blueName + " WON";
            winText.color = Color.blue;
            if (PhotonNetwork.IsMasterClient) //If we are blue
            {
                AudioManager.instance.Play("Win" + Random.Range(1, 3));
            }
            else
            {
                AudioManager.instance.Play("Lose" + Random.Range(1, 4));
            }
        }
    }

    [PunRPC]
    public void EnterLevel()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void PlayAgain()
    {
        photonView.RPC("EnterLevel", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickQuit()
    {
        photonView.RPC("QuitGame", RpcTarget.AllBuffered);
    }
}
