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

    }

    public void BuildTower()
    {
        if (j > tiles.Length || onCooldown) return;
        string towerToBuild = towerPrefabs[Random.Range(0, towerPrefabs.Length)];
        PhotonNetwork.Instantiate(towerToBuild, tiles[j].position + Vector3.up * offset, Quaternion.identity);
        j++;
        StartCoroutine("CooldownCo");
    }

    IEnumerator CooldownCo()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
