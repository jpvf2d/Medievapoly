using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LuxTaxSpace : BoardSpace
{
    public int index;
    public Button okBtn;
    public TextMeshProUGUI taxTxt;
    private int taxAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        okBtn.onClick.AddListener(taxActions);  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void passing()
    {
        Debug.Log("Do nothing, land:LuxTaxSpace");
    }

    public override void land()
    {            
        taxTxt.text = "Pay luxury tax (Lose $300)";
        GameplaySystem.TaxCard.SetActive(true);  
    }

    private void taxActions()
    {

        if (GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().boardSpaceIndex != index) 
        {
            return;
        }

        if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money < taxAmount)
        {
            GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money = 0;
            ActionTextScript.display("You're out of funds!");
        }

        else
        {
            taxAmount = 300; 
            GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= taxAmount;
        }

        
        GameplaySystem.TaxCard.SetActive(false);
        SpaceLogic.continue_sl = true;
    }

    public override void stuck()
    {
        Debug.Log("Do nothing, stuck:LuxTaxSpace");
    }

    
}
