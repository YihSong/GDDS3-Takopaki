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
    int redTargetsLeft;
    int blueTargetsLeft;
    [SerializeField] int redTargets = 12;
    public enum GameState { PREGAME, INGAME, POSTGAME, OVERTIME }
    public GameState state;
    [SerializeField] float timeLeft;
    [SerializeField] float timeLimit = 120f;
    [SerializeField] Text timeText;


    void Start()
    {
        pv = GetComponent<PhotonView>();
        redTargetsLeft = 6;
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
                case GameState.PREGAME:
                    if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
                    {
                        FindObjectOfType<MovementController>().disableInputs = true;
                        Debug.Log("Less than 2 players");
                    }
                    else
                    {
                        Debug.Log("Enough players to start");
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, false);
                            Debug.Log("Enabling a player's movement");
                        }
                        Debug.Log("Changing state");
                        AudioManager.instance.Play("In Game BGM");
                        state = GameState.INGAME;
                    }
                    break;
                case GameState.INGAME:
                    timeLeft -= Time.deltaTime;
                    pv.RPC("UpdateUI", RpcTarget.AllBuffered, timeLeft);
                    if (timeLeft <= 0)
                    {
                        if (scoreBar.fillAmount == 0.5f)
                        {
                            pv.RPC("UpdateUIString", RpcTarget.AllBuffered, "OVERTIME!!");
                            AudioManager.instance.Play("Overtime BGM");
                            state = GameState.OVERTIME;
                        }
                        else
                        {
                            foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                            {
                                ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true);
                            }
                            FindObjectOfType<RoomManager>().photonView.RPC("SetRedWon", RpcTarget.AllBuffered, scoreBar.fillAmount > 0.5f);
                            if (PhotonNetwork.IsMasterClient)
                            {
                                pv.RPC("LoadLevel", RpcTarget.AllBuffered);
                            }
                            state = GameState.POSTGAME;
                        }
                    }
                    break;
                case GameState.POSTGAME:
                    break;
                case GameState.OVERTIME:
                    if(scoreBar.fillAmount != 0.5f)
                    {
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true);
                        }
                        FindObjectOfType<RoomManager>().photonView.RPC("SetRedWon", RpcTarget.AllBuffered, scoreBar.fillAmount > 0.5f);
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
            blueTargetsLeft++;
        }
        scoreBar.fillAmount = (float) (redTargetsLeft / blueTargetsLeft) / 2;
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
}
