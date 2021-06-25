using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;   //access photon classes to connect to server
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;
    public GameObject UsernamePanel;
    public GameObject LobbyInfo;
    public GameObject ConnectButton;

    [SerializeField] Text lobbyText;
    [SerializeField]
    public InputField  CreateRoomInput, JoinRoomInput, UsernameInput;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListPrefab;
    List<GameObject> playerListItems;

    #region Unity Methods


    private void Awake()
    {
        // All clients in the same room sync their level automatically
        // When I create the room, I will be the Master Client/king in this room 
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        UsernamePanel.SetActive(false);
        LobbyInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom) return;
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            ConnectButton.SetActive(true);
        }
        else
        {
            ConnectButton.SetActive(false);
        }
    }

    #endregion


    #region Public Methods

    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
            
        }      
    }

    //Join random room
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void EnterUsername()
    {
        if(UsernameInput.text == "")
        {
            return;
        }
        PhotonNetwork.NickName = UsernameInput.text;
        UsernamePanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }

    public void CloseLobbyInfo()
    {
        LobbyInfo.SetActive(false);
    }

    #endregion



    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to photon server.");
        //LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        UsernamePanel.SetActive(true);
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    // Called on entering a lobby on the Master Server. The actual room-list updates will call OnRoomListUpdate.
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby!!!");
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            GameObject go = Instantiate(playerListPrefab, playerListContent);
            go.GetComponent<PlayerListItem>().SetUp(players[i]);
            playerListItems.Add(go);
        }

        //PhotonNetwork.LoadLevel(1);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " "+ PhotonNetwork.CurrentRoom.PlayerCount);
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Destroy(playerListItems[i]);
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    [PunRPC]
    public void EnterLevel()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void OnClickConnect()
    {
        photonView.RPC("EnterLevel", RpcTarget.AllBuffered);
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(Disconnect());
    }

    IEnumerator Disconnect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        LobbyInfo.SetActive(false);
    }




    #endregion


    #region Private methods
    public void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + CreateRoomInput.text;
        lobbyText.text = CreateRoomInput.text;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(CreateRoomInput.text, roomOptions);
        LobbyInfo.SetActive(true);
    }

    public void Onclick_JoinRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(JoinRoomInput.text, ro, TypedLobby.Default);
    }



    #endregion

}
