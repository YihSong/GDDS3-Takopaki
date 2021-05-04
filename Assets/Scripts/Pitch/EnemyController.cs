using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject floatingTextPrefab;

    public GameObject fireParticle;
    //public GameObject groundParticle;
    //public GameObject waterParticle;
    public GameObject electricParticle;

    public BulletController bullet;

    int damageToTake;

    public bool electrocuted;
    public bool burn;
    public bool wet;
    public bool grounded;

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

     public void ShowCombustionTrigger ()
    {
        if (floatingTextPrefab)
        {
            var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = "Combustion";

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Elemental Triggers
        // (have to manually change floating text name)
        if (burn && electrocuted == true)
        {
            damageToTake = damageToTake * 2;
            print("Fire Electric Combo");

            
        }

        if (burn == true)
        {
            fireParticle.SetActive(true);
        }
        else
        {
            fireParticle.SetActive(false);
        }

        if (electrocuted == true)
        {
            electricParticle.SetActive(true);
        }
        else
        {
            electricParticle.SetActive(false);
        }

        //----------------------------------------------------

        if (target != null)
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
        damageToTake = other.GetComponent<BulletController>().damage;

        if (other.tag == "Bullet")
        {
            health = health - damageToTake;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
