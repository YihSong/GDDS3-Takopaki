using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNameBtn : MonoBehaviour
{
    public Text RoomName;

    public LaunchManager launch;

    [SerializeField] GameObject lobbyMenu;

    // Use this for initialization
    void Start()
    {
        launch = FindObjectOfType<LaunchManager>();
        lobbyMenu = launch.LobbyInfo.gameObject;
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => OpenRoomLobby());
    }


    public void OpenRoomLobby()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
        lobbyMenu.SetActive(true);
    }

}
