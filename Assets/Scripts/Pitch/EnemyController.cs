using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //AI
    public Transform target;
    NavMeshAgent agent;
    Vector3 prevTargetPos;

    //Attributes
    [SerializeField]int health;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            UpdateMovingTarget();
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
        agent.SetDestination(target.transform.position);
        prevTargetPos = target.position;
    }

    void UpdateMovingTarget()
    {
        if(Vector3.Distance(prevTargetPos, target.position) >= 0.2f)
        {
            agent.SetDestination(target.position);
            prevTargetPos = target.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
