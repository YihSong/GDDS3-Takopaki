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
    [SerializeField] int redTargets = 12;
    public enum GameState { PREGAME, INGAME, POSTGAME}
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
                    if(PhotonNetwork.CurrentRoom.PlayerCount < 2)
                    {
                        FindObjectOfType<MovementController>().disableInputs = true;
                        Debug.Log("Less than 2 players");
                    }
                    else
                    {
                        Debug.Log("Enough players to start");
                        foreach(PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, false);
                            Debug.Log("Enabling a player's movement");
                        }
                        Debug.Log("Changing state");
                        state = GameState.INGAME;
                    }
                    return;
                case GameState.INGAME:
                    timeLeft -= Time.deltaTime;
                    pv.RPC("UpdateUI", RpcTarget.AllBuffered);
                    if(timeLeft <= 0)
                    {
                        foreach (PlayerSetup ps in FindObjectsOfType<PlayerSetup>())
                        {
                            ps.photonView.RPC("EnableDisableInput", RpcTarget.AllBuffered, true);
                        }
                        state = GameState.POSTGAME;
                    }
                    return;
                case GameState.POSTGAME:
                    return;
            }
        }
    }

    public void GainLoseTarget(bool losing)
    {
        if (losing)
        {
            redTargetsLeft--;
        }
        else
        {
            redTargetsLeft++;
        }
        scoreBar.fillAmount = (float) redTargetsLeft / redTargets;
    }

    [PunRPC]
    public void UpdateUI()
    {
        timeText.text = "" + (int) timeLeft;
    }
}
