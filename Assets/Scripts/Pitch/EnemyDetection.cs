using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float enemyAttackSpeed;

    public bool playerInRadius;

    public PlayerController thePlayer;
    float timeElapsed;

    private void OnTriggerEnter(Collider other)
    {

        playerInRadius = true;
        thePlayer = other.GetComponent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRadius = false;
        thePlayer = null;
    }

    void Update()
    {
        
        if (playerInRadius == true)
        {
            if (timeElapsed >= enemyAttackSpeed)
            {
                thePlayer.health = thePlayer.health - 10;
                timeElapsed = 0;
            }
            timeElapsed += Time.deltaTime;
            //StartCoroutine("HitPlayer");

        }
    }

    //IEnumerator HitPlayer()
    //{
    //    thePlayer.health = thePlayer.health - 10;
    //   yield return new WaitForSeconds(enemyAttackSpeed);
    //}
} 
