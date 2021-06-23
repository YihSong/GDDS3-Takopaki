using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ground : MonoBehaviourPunCallbacks
{
    [SerializeField] Material blue, red;
    [SerializeField] bool startsRed;
    bool flipped;
    Renderer r;
    [SerializeField] int numOfTargets;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void FlipColor()
    {
        if (startsRed)
        {
            if (flipped)
            {
                r.material = red;
                flipped = false;
            }
            else
            {
                r.material = blue;
                flipped = true;
            }
        }
        else
        {
            if (flipped)
            {
                r.material = blue;
                flipped = false;
            }
            else
            {
                r.material = red;
                flipped = true;
            }
        }
    }

    [PunRPC]
    public void LoseTarget()
    {
        numOfTargets--;
        if(numOfTargets <= 0)
        {
            photonView.RPC("FlipColor", RpcTarget.AllBuffered);
        }
    }
}
