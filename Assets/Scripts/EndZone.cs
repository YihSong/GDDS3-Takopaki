using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public GameMaster gM;

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            gM.collectiveHealth -= 1;
        }
    }
}
