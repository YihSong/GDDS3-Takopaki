using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TradeConfirmation : MonoBehaviour
{
    TowerBuilder towerBuilder;

    public bool mySideConfirm;
    public bool otherSideConfirm;

    void Start()
    {
        TowerBuilder[] builders = FindObjectsOfType<TowerBuilder>();
        foreach (TowerBuilder t in builders)
        {
            if (t.gameObject.GetComponent<PhotonView>().IsMine)
            {
                towerBuilder = t;
                break;
            }
        }
    }

    void Update()
    {

    }

    public void ConfirmTrade()
    {

        mySideConfirm = !mySideConfirm;
        GetComponent<PhotonView>().RPC("OtherConfirmTrade", RpcTarget.Others);
    }

    [PunRPC]
    public void OtherConfirmTrade()
    {
        otherSideConfirm = !otherSideConfirm;
    }
}
