using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    LaunchManager lobbyInfo;

    public void SetUp(LaunchManager _lobbyInfo)
    {
        lobbyInfo = _lobbyInfo;
        text.text = lobbyInfo.CreateRoomInput.text;
    }

    public void OnClick()
    {

    }
}
