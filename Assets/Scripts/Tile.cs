using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tile : MonoBehaviour
{

    public bool tile1;
    public bool tile2;
    public bool tile3;
    public bool tile4;
    public bool tile5;
    public bool tile6;
    public bool tile7;
    public bool tile8;
    public bool tile9;
    public bool tile10;
    public bool tile11;
    public bool tile12;
    public bool tile13;
    public bool tile14;

    public bool otherSelected;
    public bool selected;
    public bool towerPresent;

    public PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
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
        if (towerPresent == true && selected == false)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        if (towerPresent == true && selected == false)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (pv.IsMine)
        {
            if (towerPresent == true)
            {
                selected = true;
            }
            else
            {
                selected = false;
            }
        }

        if (!pv.IsMine)
        {
            if (towerPresent == true)
            {
                otherSelected = true;
            }
            else
            {
                otherSelected = false;
            }
        }
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
        }
           

    }

}
