using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public Transform[] tiles;
    public LayerMask layer;
    public float offset;
    public bool onCooldown = false;
    public float cooldown;
    int j = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject towerToBuild = towerPrefabs[Random.Range(0, towerPrefabs.Length)];
        Instantiate(towerToBuild, tiles[j].position + Vector3.up * offset, Quaternion.identity);
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
