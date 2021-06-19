using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager Instance;

    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;


    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)
        {
            //print("found scene");
                if(PhotonNetwork.CurrentRoom.PlayerCount >= 2)
                {
                    PhotonNetwork.Instantiate("1 side tiles", Vector3.zero, Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    PhotonNetwork.Instantiate("1 side tiles", Vector3.zero, Quaternion.identity);
                }
                
            
        }
    }
}
