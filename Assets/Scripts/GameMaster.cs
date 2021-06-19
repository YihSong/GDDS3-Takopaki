using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


public class GameMaster : MonoBehaviour
{
    public int collectiveHealth;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public PhotonView pv;

    

    void Start()
    {
        collectiveHealth = 3;

        pv = GetComponent<PhotonView>();
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    Destroy(gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //    if (collectiveHealth == 3)
        //    {
        //        heart1.SetActive(true);
        //        heart2.SetActive(true);
        //        heart3.SetActive(true);
        //    }
        //    else if (collectiveHealth == 2)
        //    {
        //        heart1.SetActive(false);
        //        heart2.SetActive(true);
        //        heart3.SetActive(true);
        //    }
        //    else if (collectiveHealth == 1)
        //    {
        //        heart1.SetActive(false);
        //        heart2.SetActive(false);
        //        heart3.SetActive(true);
        //    }
        //    else if (collectiveHealth <= 0)
        //    {
        //        heart1.SetActive(false);
        //        heart2.SetActive(false);
        //        heart3.SetActive(false);
        //    }
        //}
    }
}
