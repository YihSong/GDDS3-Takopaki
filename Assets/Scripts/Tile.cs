using System.Collections;
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
            if (selected == true)
            {
                Debug.Log("Unselecting");
                selected = false;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            else
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

    public void ClearCurrentTower()
    {
        clearTower = true;
    }
}
