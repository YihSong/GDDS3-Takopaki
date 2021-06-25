using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager Instance;
    [SerializeField] GameObject playerManager;
    public bool redWon = false;

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
            PhotonNetwork.Instantiate(playerManager.name, Vector3.zero, Quaternion.identity);
        }
        else if (scene.buildIndex == 2)
        {
            FindObjectOfType<EndScreenManager>().SetEndScreen(PhotonNetwork.PlayerList[0].NickName, PhotonNetwork.PlayerList[1].NickName, redWon);
        }
    }
}
