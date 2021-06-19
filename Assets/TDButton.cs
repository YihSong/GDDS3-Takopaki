using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TDButton : MonoBehaviour
{
    TowerBuilder towerBuilder;

    // Start is called before the first frame update
    void Start()
    {
        TowerBuilder[] builders = FindObjectsOfType<TowerBuilder>();
        foreach(TowerBuilder t in builders)
        {
            if (t.gameObject.GetComponent<PhotonView>().IsMine)
            {
                towerBuilder = t;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTower()
    {
        towerBuilder.BuildTower();
    }
}
