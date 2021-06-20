using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Info", menuName = "ScriptableObjects/TowerInfo", order = 1)]
public class TowerInfo : ScriptableObject
{
    public float range;
    public string enemyTag = "Enemy";
    public LayerMask layerMask;

    public float turnSpeed = 10f;
    public int damage;
    public float fireRate;
}
