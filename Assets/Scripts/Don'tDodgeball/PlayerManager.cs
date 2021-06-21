using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] Vector3 spawnPos1;
    [SerializeField] Vector3 spawnPos2;
    [SerializeField] GameObject playerPrefab;
    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) return;
        if(PhotonNetwork.IsConnected && playerPrefab != null)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, spawnPos1, Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(playerPrefab.name, spawnPos2, Quaternion.Euler(0, 180, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
