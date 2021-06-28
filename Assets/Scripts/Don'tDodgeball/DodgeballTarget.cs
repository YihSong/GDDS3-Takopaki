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
            foreach (Camera c in FindObjectsOfType<Camera>()) //Find all cameras in scene
            {
                Debug.Log("Found a camera");
                if (c.enabled) //If camera is enabled, it's the camera on our local client
                {
                    Debug.Log("Found our camera");
                    cam = c;
                    break;
                }
            }
        }
        else
        {
            Vector3 lookPos = new Vector3(cam.transform.position.y, healthBar.transform.parent.position.y, cam.transform.position.z); //Make sure LookAt doesnt make us rotate up or down
            healthBar.transform.parent.LookAt(lookPos); //Look at the camera on our local client
        }
    }

    [PunRPC]
    public void TakeDamage(int _damage) //Since we RPC this, everything that gets called by this doesnt have to be RPCed
    {
        health -= _damage;
        Debug.Log("Taking damage: " + _damage + "Remaining health: " + health);
        healthBar.fillAmount = (float) health / startHealth; //Update health manager
        if(health <= 0)
        {
            StartCoroutine(BlowUp());
        }
    }

    IEnumerator BlowUp()
    {
        Explosion(); //Explosion fx
        yield return new WaitForSeconds(2.5f); //Wait for explosion to finish before destroying
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
        ground.LoseTarget(); //Tell the ground we died so it can change color
        gameMaster.GainLoseTarget(isRed); //Tell the game master to update targets left based on wether we are red
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
