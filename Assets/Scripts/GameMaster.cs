using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class GameMaster : MonoBehaviour
{

    public PhotonView pv;
    [SerializeField] Image scoreBar;
    [SerializeField] int redTargetsLeft;
    int blueTargetsLeft;
    [SerializeField] int redTargets = 12;
    public enum GameState { PREGAME, INGAME, POSTGAME, OVERTIME }
    public GameState state;
    [SerializeField] float timeLeft;
    [SerializeField] float timeLimit = 120f;
    [SerializeField] Text timeText;

    public GameObject settings;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        redTargetsLeft = 6;
        blueTargetsLeft = 6;
        if (pv.IsMine)
        {
            state = GameState.PREGAME;
            timeLeft = timeLimit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            switch (state)
            {
                case GameState.PREGAME: //Before the game starts
                    if (PhotonNetwork.CurrentRoom.PlayerCount < 2) //If we dont have enough players
                    {
                        FindObjectOfType<MovementController>().disableInputs = true; //Disable player movement
                        Debug.Log("Less than 2 players");
                    }
                    else
                    {
                        Debug.Log("Enough players to start");
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>()) //Look at all the players in the scene
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, false); //Enable each player movement
                            Debug.Log("Enabling a player's movement");
                        }
                        Debug.Log("Changing state");
                        AudioManager.instance.Stop("Main Menu BGM"); //Stop Main Menu BGM
                        AudioManager.instance.Play("In Game BGM"); //Play Game BGM
                        state = GameState.INGAME; //Change state to ingame
                    }
                    break;
                case GameState.INGAME: //During play time
                    timeLeft -= Time.deltaTime; //Deduct timeLeft by time passed between frame
                    pv.RPC("UpdateUI", RpcTarget.AllBuffered, timeLeft); //Update the timer on both clients
                    if (redTargetsLeft <= 0) //If all red targets got destroyed, red won
                    {
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true); //Disable all players input
                        } 
                        FindObjectOfType<RoomManager>().photonView.RPC("SetRedWon", RpcTarget.AllBuffered, false); //Tell the end scene that blue won
                        if (PhotonNetwork.IsMasterClient) //Make sure only the master client sends the RPC
                        {
                            pv.RPC("LoadLevel", RpcTarget.AllBuffered); //Tell all clients to load the next scene
                        }
                        state = GameState.POSTGAME;
                    }
                    else if(blueTargetsLeft <= 0) //If all blue targets destroyed, blue lost
                    {
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true); //Disable all players input
                        }
                        FindObjectOfType<RoomManager>().photonView.RPC("SetRedWon", RpcTarget.AllBuffered, true); //Tell the end scene that red won
                        if (PhotonNetwork.IsMasterClient) //Make sure only the master client sends the RPC
                        {
                            pv.RPC("LoadLevel", RpcTarget.AllBuffered); //Tell all clients to load the next scene
                        }
                        state = GameState.POSTGAME;
                    }
                    if (timeLeft <= 0) //When play time is over
                    {
                        if (scoreBar.fillAmount == 0.5f) //If the game is currently in a draw, enter overtime, first to break something wins
                        {
                            pv.RPC("UpdateUIString", RpcTarget.AllBuffered, "OVERTIME!!"); //Tell both clients to change timer to OVERTIME
                            AudioManager.instance.Stop("In Game BGM");
                            AudioManager.instance.Play("Overtime BGM");
                            AudioManager.instance.overtime = true; //Tell audiomanager we are in overtime for pausing and playing bgm
                            state = GameState.OVERTIME;
                        }
                        else //If the game is not in a draw, someone wins
                        {
                            foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                            {
                                ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true);
                            }
                            FindObjectOfType<RoomManager>().photonView.RPC("SetRedWon", RpcTarget.AllBuffered, scoreBar.fillAmount > 0.5f); //Tell end screen who won based on score, score calculated by ratio of red targets to blue targets
                            if (PhotonNetwork.IsMasterClient)
                            {
                                pv.RPC("LoadLevel", RpcTarget.AllBuffered); //Tell all clients to load next scene
                            }
                            state = GameState.POSTGAME;
                        }
                    }
                    break;
                case GameState.POSTGAME:
                    break;
                case GameState.OVERTIME:
                    if(scoreBar.fillAmount != 0.5f) //Once someone breaks something, someone won
                    {
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true);
                        }
                        FindObjectOfType<RoomManager>().photonView.RPC("SetRedWon", RpcTarget.AllBuffered, scoreBar.fillAmount > 0.5f); //Calculte who won and send to end screen
                        if (PhotonNetwork.IsMasterClient)
                        {
                            pv.RPC("LoadLevel", RpcTarget.AllBuffered);
                        }
                        state = GameState.POSTGAME;
                    }
                    break;
            }
        }
    }

    [PunRPC]
    public void LoadLevel()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void GainLoseTarget(bool losing)
    {
        if (losing)
        {
            redTargetsLeft--;
        }
        else
        {
            blueTargetsLeft--;
        }

        float blue = (float)blueTargetsLeft;
        float red = (float)redTargetsLeft;

        scoreBar.fillAmount = (red / blue) / 2f;
    }

    [PunRPC]
    public void UpdateUI(float time)
    {
        timeText.text = "" + (int) time;
    }

    [PunRPC]
    public void UpdateUIString(string text)
    {
        timeText.text = text;
    }

    [PunRPC]
    public void Forfeit(bool blueForfeit)
    {
        if (blueForfeit)
        {
            blueTargetsLeft = 0;
        }
        else redTargetsLeft = 0;
    }

    public void ClickForfeit()
    {
        if (PhotonNetwork.IsMasterClient) //if we are master client, we are blue
        {
            Forfeit(true); //GameMaster in masterclient handles everything so no RPC if we are master client
        }
        else //If we are not master client, we are red
        {
            pv.RPC("Forfeit", RpcTarget.MasterClient, false); //Tell the game master in the master client that we forfeit
        }
    }

    public void ShowSettings()
    {
        settings.SetActive(true);
    }
}
