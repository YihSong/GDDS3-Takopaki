using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform target;
    public float moveSpeed;
    public float turnSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            //Move to target
            if (Vector3.Distance(transform.position, target.position) > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }

            else
            {
                //TODO: attack
            }

            //Look at target
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        }

        else
        {
            FindTarget();
        }
    }

    void FindTarget()
    {
        PlayerController temp = null;
        foreach(PlayerController player in FindObjectsOfType<PlayerController>())
        {
            if(temp == null)
            {
                temp = player;
            }

            if(Vector3.Distance(transform.position, player.transform.position) < Vector3.Distance(transform.position, temp.transform.position))
            {
                temp = player;
            }
        }
        target = temp.transform;
    }
}
