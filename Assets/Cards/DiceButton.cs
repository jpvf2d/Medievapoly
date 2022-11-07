using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;

public class DiceButton : MonoBehaviour
{
	private SpriteRenderer rend;
	private Sprite diceSprite;
	public Dice Dice1;
	public Dice Dice2;
	private int turn = 1; 
	private bool coroutine = true;
	
    // Start is called before the first frame update
    void Start()
    {
		rend = GetComponent<SpriteRenderer>();
        diceSprite = Resources.Load<Sprite>("DiceButton/newDiceButton");
        rend.sprite = diceSprite;
		
    }

    private void OnMouseDown() 
    {
		if(coroutine)
		{
			coroutine = false;
			GameplaySystem.roll();
			StartCoroutine(MoveThePlayers());
		}
    }

	public void Roll()
	{
		coroutine = false;
		GameplaySystem.roll();
		StartCoroutine(MoveThePlayers());
	}
	
	private IEnumerator MoveThePlayers()
	{
		while(Dice1.db_coroutine == false || Dice2.db_coroutine == false)
		{
			yield return new WaitForSeconds(0.05f);
		}
		GameplaySystem.diceSideThrown = Dice1.DiceNum + Dice2.DiceNum + 2;
		int turn = GameplaySystem.turn;

		// Options for player if player is in jail 
		if(GameplaySystem.players[turn].GetComponent<Player>().stuckInPlace == true)
		{
			//If player rolls doubles or has 'get out of jail' card, escape Jail
			if(Dice1.DiceNum == Dice2.DiceNum || GameplaySystem.players[turn].GetComponent<Player>().jailFreeCard)
			{
				JailSpace.escapeJail = true; 
				GameplaySystem.MovePlayer();

				if(Dice1.DiceNum != Dice2.DiceNum) //Check if player had to use 'get out of jail' card 
				{
					GameplaySystem.players[turn].GetComponent<Player>().jailFreeCard = false; 
				}
			}

			//Negotiate for a card from another player 
			else if(Array.Exists(JailSpace.availableJailCards, x => x.Equals(1)))
			{
				for(int i = 0; i < JailSpace.availableJailCards.Length; i++)
				{
					if(JailSpace.availableJailCards[i] == 1) //Attempt to bargain with each player who has a card
					{
						JailCardDisplay.activateJailCardDisplay = true; 
						JailCardDisplay.jailCardOwner = i; 	
						while(!JailCardDisplay.cont_jcd)
						{
							yield return new WaitForSeconds(0.05f); 
						}
						JailCardDisplay.cont_jcd = false; 

						//If jailed player did not want to negotiate or tried to offer too much money 
						if(JailCardDisplay.noNeg || JailCardDisplay.overBargained)
						{
							if(JailCardDisplay.overBargained)
								Debug.Log("Overdidit");
							break; 
						}

						//If a succesful negotiation occured, escape 
						if(JailCardDisplay.negOver)
						{
							JailSpace.escapeJail = true; 
							GameplaySystem.MovePlayer();
							break; 
						}
					}
					yield return new WaitForSeconds(0.05f);
				}

				if(JailCardDisplay.overBargained)
				{
					JailSpace.waitInJail = true; 
				}

				//Player didn't want to negotiate or failed to successfully, can pay fine to escape
				else if(JailCardDisplay.noNeg || !JailCardDisplay.negOver)
				{
					StartCoroutine("payToEscape");
				}

				//Reset bools in 'JailCardDisplay' 
				JailCardDisplay.noNeg = false; 
				JailCardDisplay.negOver = false; 
				JailCardDisplay.overBargained = false; 
			}

			else
			{
				StartCoroutine("payToEscape");
			}
		}

		//If player not in jail, just move player 
		else 
		{
       		GameplaySystem.MovePlayer();
		} 

		Dice1.db_coroutine = false; 
		Dice2.db_coroutine = false;
		coroutine = true;
	}

	//Pay $50 to escape jail 
	private IEnumerator payToEscape()
	{
		if(GameplaySystem.players[turn].GetComponent<Player>().money > 50
		   && !GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().autoPlayEnabled)
			JailFeeDisplay.payJailFee.SetActive(true);
		else
			JailFeeDisplay.cont_jailfee = true;

		while(!JailFeeDisplay.cont_jailfee)
		{
			yield return new WaitForSeconds(0.01f);
		}
		JailFeeDisplay.cont_jailfee = false;

		if(JailFeeDisplay.paidFee)
		{
			JailSpace.escapeJail = true;
			GameplaySystem.MovePlayer();
		}

		else
		{
			JailSpace.waitInJail = true; 
		}
	}	
}

