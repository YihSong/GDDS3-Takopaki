using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DodgeballTarget : MonoBehaviour
{
    [SerializeField] int startHealth;
    int health;
    [SerializeField] Image healthBar;
    Camera camera;

    private void Start()
    {
        health = startHealth;
    }

    private void Update()
    {
        if(camera == null)
        {
            foreach (Camera c in FindObjectsOfType<Camera>())
            {
                Debug.Log("Found a camera");
                if (c.enabled)
                {
                    Debug.Log("Found our camera");
                    camera = c;
                    break;
                }
            }
        }
        else
        {
            Vector3 lookPos = new Vector3(camera.transform.position.y, healthBar.transform.parent.position.y, camera.transform.position.z);
            healthBar.transform.parent.LookAt(lookPos);
        }
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        Debug.Log("Taking damage: " + _damage + "Remaining health: " + health);
        healthBar.fillAmount = (float) health / startHealth;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (transform.parent)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
