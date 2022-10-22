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
        this.cardChoice = Random.Range(0,2);

        switch(cardChoice)
        {
            case 0:
                chanceTxt.text = "Advance to Go (Collect $200)";
                break;
            case 1:
                chanceTxt.text = "Pay every player $25";
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
                // GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().boardSpaceIndex = 0;
                // GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().move = true;
                // GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().Move();

                // GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().relocated = true;
                break;
            
            // Pay every player $25
            case 1:
                // give $25 to every player
                for(int i = 0; i < GameplaySystem.numPlayers; i++) {
                    if(i != GameplaySystem.turn) {
                        GameplaySystem.players[i].GetComponent<Player>().money += 25;
                    }
                }
                // take that money to current player
                GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 25 * (GameplaySystem.numPlayers - 1);
                break;
		}

        GameplaySystem.ChanceCard.SetActive(false);
        SpaceLogic.continue_sl = true;  
    }
}
