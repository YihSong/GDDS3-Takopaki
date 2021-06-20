using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    public GameObject player1Confirmation;
    public TowerBuilder td;

    public GameObject tradeButton;

    public TradeConfirmation confirmation;
    private void Start()
    {
        td = FindObjectOfType<TowerBuilder>();
        confirmation = FindObjectOfType<TradeConfirmation>();
    }
    // Update is called once per frame
    void Update()
    {
        if(td.tilesAreSelected == true)
        {
            player1Confirmation.SetActive(true);
        }
        else
        {
            player1Confirmation.SetActive(false);
        }

        if (confirmation.mySideConfirm == true && confirmation.otherSideConfirm == true)
        {
            tradeButton.SetActive(true);
        }
        else
        {
            tradeButton.SetActive(false);
        }
    }
}
