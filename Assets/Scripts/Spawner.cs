using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviour
{

    public PhotonView pv;
    public string [] enemies;


    public void Start()
    {
        InvokeRepeating("Spawn", 1f, 2f);
        pv = GetComponent<PhotonView>();
    }

    public void Update()
    {

    }

    public void Spawn()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
            }

        }

    }
}
