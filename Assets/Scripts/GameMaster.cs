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
                    if(PhotonNetwork.CountOfPlayersInRooms < 2 || FindObjectsOfType<MovementController>().Length < 2)
                    {
                        FindObjectOfType<MovementController>().disableInputs = true;
                    }
                    else
                    {
                        foreach(MovementController mc in FindObjectsOfType<MovementController>())
                        {
                            mc.pv.RPC("EnableInputs", RpcTarget.AllBuffered, false);
                        }
                        state = GameState.INGAME;
                    }
                    break;
                case GameState.INGAME:
                    timeLeft -= Time.deltaTime;
                    pv.RPC("UpdateUI", RpcTarget.AllBuffered);
                    if(timeLeft <= 0)
                    {
                        foreach (MovementController mc in FindObjectsOfType<MovementController>())
                        {
                            mc.pv.RPC("EnableInputs", RpcTarget.AllBuffered, true);
                            state = GameState.POSTGAME;
                        }
                    }
                    break;
                case GameState.POSTGAME:
                    break;
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
