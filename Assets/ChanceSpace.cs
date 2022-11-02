using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChanceSpace : BoardSpace
{
    public int index;
    public Button okBtn;
    public TextMeshProUGUI chanceTxt;
    private int cardChoice = 0;

    // Start is called before the first frame update
    void Start()
    {
		okBtn.onClick.AddListener(chanceActions);       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void passing()
    {
        Debug.Log("Do nothing, land:ChanceSpace");
    }

    public override void land()
    {
        // display chance card
        this.cardChoice = Random.Range(0,6);

        switch(cardChoice)
        {
            case 0:
                chanceTxt.text = "Advance to Go (Collect $200)";
                break;
            case 1:
                chanceTxt.text = "Pay every player $25";
                break;
            case 2:
                chanceTxt.text = "Pay hospital bill of $100";
                break;
            case 3:
                chanceTxt.text = "Go to Free Parking";
                break;
            case 4:
                chanceTxt.text = "Donate $50 to charity";
                break;
            case 5:
                chanceTxt.text = "Get out of jail free card";
                break;
        }  

        GameplaySystem.ChanceCard.SetActive(true);
    }

    public override void stuck()
    {
        Debug.Log("Do nothing, stuck:ChanceSpace");
    }

    public void chanceActions() {
        if (GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().boardSpaceIndex != index) {
            return;
        }

		switch(cardChoice)
		{
		    // Advance to Go (Collect $200)
            case 0:
                // this.chanceTxt = "Advance to Go (Collect $200)";
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += 200;
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().boardSpaceIndex = 0;
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().relocated = true;
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().move = true;
                break;
            
            // Pay every player $25
            case 1:
                // give $25 to every player
                for(int i = 0; i < GameplaySystem.numPlayers; i++) {
                    if(i != GameplaySystem.turn && !GameplaySystem.players[i].GetComponent<Player>().isDead) {
                        GameplaySystem.players[i].GetComponent<Player>().money += 25;
                    }
                }
                // take that money to current player
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 25 * (GameplaySystem.alivePlayers - 1);
                break;

            // Pay hospital bill of $100
            case 2:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 100;
                break;
            
            // Go to Free Parking
            case 3:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().boardSpaceIndex = 39;
                GameplaySystem.playerIndex = 39;
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().relocated = true;
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().tmpMoveSpeed = 70f;
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().move = true;
                break;

            // Donate $50 to charity
            case 4:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 50;
                break;

            //Get out of jail free card
            case 5:
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().jailFreeCard = true;
                JailSpace.availableJailCards[GameplaySystem.turn] = 1; 
                break;
		}

        GameplaySystem.ChanceCard.SetActive(false);
        SpaceLogic.continue_sl = true;  
    }
}
