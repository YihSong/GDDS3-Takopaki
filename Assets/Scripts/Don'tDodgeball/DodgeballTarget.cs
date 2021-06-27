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
    Camera cam;
    [SerializeField] Ground ground;
    [SerializeField] bool isRed;
    [SerializeField] GameObject explosionFx;
    GameMaster gameMaster;

    private void Start()
    {
        health = startHealth;
        gameMaster = FindObjectOfType<GameMaster>();
        explosionFx.SetActive(false);
    }

    private void Update()
    {
        if(cam == null)
        {
            foreach (Camera c in FindObjectsOfType<Camera>())
            {
                Debug.Log("Found a camera");
                if (c.enabled)
                {
                    Debug.Log("Found our camera");
                    cam = c;
                    break;
                }
            }
        }
        else
        {
            Vector3 lookPos = new Vector3(cam.transform.position.y, healthBar.transform.parent.position.y, cam.transform.position.z);
            healthBar.transform.parent.LookAt(lookPos);
        }
    }

    [PunRPC]
    public void TakeDamage(int _damage) //Since we RPC this, everything that gets called by this doesnt have to be RPCed
    {
        health -= _damage;
        Debug.Log("Taking damage: " + _damage + "Remaining health: " + health);
        healthBar.fillAmount = (float) health / startHealth;
        if(health <= 0)
        {
            StartCoroutine(BlowUp());
        }
    }

    IEnumerator BlowUp()
    {
        Explosion();
        yield return new WaitForSeconds(2.5f);
        Die();
    }

    [PunRPC]
    public void Explosion()
    {
        explosionFx.SetActive(true);
        AudioManager.instance.Play("Explode");
    }

    void Die()
    {
        ground.LoseTarget();
        gameMaster.GainLoseTarget(isRed);
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
