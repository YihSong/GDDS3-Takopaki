﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tile : MonoBehaviour
{

    public int tileNumber;

    public bool otherSelected;
    public bool selected;
    public bool towerPresent;

    public bool doubleshot;
    public bool defaulttower;
    public bool bombtower;

    public PhotonView pv;

    bool clearTower;

    public GameObject tower;
    TowerBuilder towerBuilder;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        towerBuilder = GetComponentInParent<TowerBuilder>();
    }
    void Update()
    {
        if (selected == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnMouseOver()
    {
        if (towerPresent == true && selected == false && pv.IsMine)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        if (towerPresent == true && selected == false && pv.IsMine)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (pv.IsMine)
        {
            if (selected == true)
            {
                Debug.Log("Unselecting");
                selected = false;
                towerBuilder.selectedTile = null;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                if (towerPresent == true)
                {
                    pv.RPC("PlayerSelected", RpcTarget.Others);
                    selected = true;
                    if (towerBuilder.selectedTile != null)
                    {
                        towerBuilder.selectedTile.selected = false;
                        towerBuilder.selectedTile.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                    towerBuilder.selectedTile = this;
                }
                else
                {
                    selected = false;
                    towerBuilder.selectedTile = null;
                }
            }
        }
    }

    [PunRPC]
    public void OtherPlayerSelected()
    {
        Debug.Log("RPC Received");
        selected = true;
        if (towerBuilder.selectedTile != null)
        {
            towerBuilder.selectedTile.selected = false;
            towerBuilder.selectedTile.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        towerBuilder.selectedTile = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tower")
        {
            towerPresent = true;
        }
        else
        {
            towerPresent = false;
            clearTower = false;
        }
           
        if (other.name == "Default Turret(Clone)")
        {
            defaulttower = true;
        }
        else if (other.name == "Double Shot Turret(Clone)")
        {
            doubleshot = true;
        }
        else if (other.name == "Bomb Turret(Clone)")
        {
            bombtower = true;
        }

        if (clearTower == true)
        {
            Destroy(other.gameObject);
        }
    }

    [PunRPC]
    public void ClearCurrentTower()
    {
        Destroy(tower);
        Debug.Log("Unselecting");
        selected = false;
        towerBuilder.selectedTile = null;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
