using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TowerBuilder : MonoBehaviour
{
    public string[] towerPrefabs;
    public Transform[] tiles;
    public LayerMask layer;
    public float offset;
    public bool onCooldown = false;
    public float cooldown;
    int j = 0;
    PhotonView pv;

    public bool tilesAreSelected;

    int tileToSpawn;
    public GameObject turretToSpawn;

    public Tile selectedTile;

    TowerBuilder otherPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            Destroy(this);
        }
        Shuffle();

    }

    void Shuffle()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            Transform temp = tiles[i];
            int rand = Random.Range(0, tiles.Length);
            tiles[i] = tiles[rand];
            tiles[rand] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            if (t.selected == true)
            {
                tilesAreSelected = true;
                break;
            }
            else
            {
                tilesAreSelected = false;
            }
        }

        if(otherPlayer == null)
        {
            foreach (TowerBuilder tb in FindObjectsOfType<TowerBuilder>())
            {
                if (!tb.GetComponent<PhotonView>().IsMine)
                {
                    otherPlayer = tb;
                    break;
                }
            }
        }
    }

    public void BuildTower()
    {
        if (j > tiles.Length || onCooldown) return;
        string towerToBuild = towerPrefabs[Random.Range(0, towerPrefabs.Length)];
        tiles[j].GetComponent<Tile>().tower =  PhotonNetwork.Instantiate(towerToBuild, tiles[j].position + Vector3.up * offset, Quaternion.identity);
        j++;
        StartCoroutine("CooldownCo");
    }

    IEnumerator CooldownCo()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public void TradeTowers()
    {
        Debug.Log("Trying to trade");
        foreach (Transform t in tiles)
        {
            Tile tile = t.GetComponent<Tile>();
            if (tile.selected == true && tile.pv.IsMine)
            {
                Debug.Log("Found tile");
                tile.ClearCurrentTower();
            }

            //PhotonNetwork.Instantiate("Default Turret", tiles[tileToSpawn] + Vector3.up * offset, Quaternion.identity);
        }

        foreach(Transform t in otherPlayer.tiles)
        {
            Tile tile = t.GetComponent<Tile>();
            if (tile.selected == true && !tile.pv.IsMine)
            {
                //take the order number of player2's selected tile and save that number into "tileToSpawn"
                tile.tileNumber = tileToSpawn;
            }
        }
    }

}
