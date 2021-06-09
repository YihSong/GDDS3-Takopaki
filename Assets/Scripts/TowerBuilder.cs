using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public LayerMask layer;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BuildTower();
        }
    }

    public void BuildTower()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            if (hit.transform.gameObject.GetComponent<Tile>().occupied) return;
            GameObject towerToBuild = towerPrefabs[Random.Range(0, towerPrefabs.Length)];
            Instantiate(towerToBuild, hit.transform.position + Vector3.up * offset, Quaternion.identity);
            hit.transform.gameObject.GetComponent<Tile>().occupied = true;
        }
    }
}
