using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChestSpace : BoardSpace
{
    public int index;
    public Button okBtn;
    public TextMeshProUGUI chestTxt;
    private int cardChoice = 0;

    // Start is called before the first frame update
    void Start()
    {
		okBtn.onClick.AddListener(chestActions);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void passing()
    {
        Debug.Log("Do nothing, land:ChestSpace");
    }

    public override void land()
    {
        // display chest card
        this.cardChoice = Random.Range(0,6);

        switch(cardChoice)
        {
            case 0:
                chestTxt.text = "Pay bill of $100";
                break;
            case 1:
                chestTxt.text = "Bank error, collect $200";
                break;
            case 2:
                chestTxt.text = "Collect $50 from every player";
                break;
            case 3:
                chestTxt.text = "Pay every player $50";
                break;
            case 4:
                chestTxt.text = "Inherit $100";
                break;
            case 5:
                chestTxt.text = "Get out of jail free card";
                break;
        }  
        
        GameplaySystem.ChestCard.SetActive(true);
    }

    public override void stuck()
    {
        Debug.Log("Do nothing, stuck:ChestSpace");
    }

    public void chestActions() {
        if (GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().boardSpaceIndex != index) {
            return;
        }

        switch(cardChoice)
        {
            // Pay bill of $100
            case 0:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 100;
                break;
            
            // Bank error, collect $200
            case 1:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += 200;
                break;
            
            // Collect $50 from every player
            case 2:
                // take $50 from every player
                for(int i = 0; i < GameplaySystem.numPlayers; i++) {
                    if(i != GameplaySystem.turn) {
                        GameplaySystem.players[i].GetComponent<Player>().money -= 50;
                    }
                }
                // give that money to current player
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += 50 * (GameplaySystem.numPlayers - 1);
                break;
            
            // Pay every player $50
            case 3:
                // give $50 to every player
                for(int i = 0; i < GameplaySystem.numPlayers; i++) {
                    if(i != GameplaySystem.turn) {
                        GameplaySystem.players[i].GetComponent<Player>().money += 50;
                    }
                }
                // take that money to current player
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 50 * (GameplaySystem.numPlayers - 1);
                break;

            // Inherit $100
            case 4:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += 100;
                break;

            //Get out of jail free card
            case 5:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().jailFreeCard = true;
                break;

        }  

        GameplaySystem.ChestCard.SetActive(false);
        SpaceLogic.continue_sl = true;  
    }
}
