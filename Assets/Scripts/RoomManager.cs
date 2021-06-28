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
            Destroy(gameObject); //make sure we are the only instance of this in the scene
            return;
        }
        DontDestroyOnLoad(gameObject); //Make this object persist through all scenes
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

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) //Called when a scene is loaded
    {
        if(scene.buildIndex == 1) //Game scene
        {
            PhotonNetwork.Instantiate(playerManager.name, Vector3.zero, Quaternion.identity); //In the game scene, spawn the player manager, which spawns the player
        }
        else if (scene.buildIndex == 2) //end scene
        {
            FindObjectOfType<EndScreenManager>().SetEndScreen(PhotonNetwork.PlayerList[0].NickName, PhotonNetwork.PlayerList[1].NickName, redWon); //In the end scene,sets who won based on the game scene
        }
    }

    [PunRPC]
    public void SetRedWon(bool _redWon)
    {
        redWon = _redWon;
    }
}
