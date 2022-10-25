using System.Collections;
using System.Collections.Generic;
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
	
	private IEnumerator MoveThePlayers()
	{
		while(Dice1.db_coroutine == false || Dice2.db_coroutine == false)
		{
			yield return new WaitForSeconds(0.05f);
		}
		
		// Options for player if player is in jail 
		if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().stuckInPlace == true)
		{

			if(Dice1.DiceNum == Dice2.DiceNum)
			{
				StartCoroutine(ActionTextScript.display("Rolled doubles, escaping the gallows"));
				JailSpace.escapeJail = true; 
				GameplaySystem.diceSideThrown = Dice1.DiceNum + Dice2.DiceNum + 2;
				GameplaySystem.MovePlayer();
			}

			else
			{
				if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money > 50)
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
					GameplaySystem.diceSideThrown = Dice1.DiceNum + Dice2.DiceNum + 2;
					GameplaySystem.MovePlayer();
				}

				else
				{
					JailSpace.waitInJail = true; 
					StartCoroutine(ActionTextScript.display("Player: " + GameplaySystem.turn + " is stuck at the gallows"));	
				}
			}
		}

		else
		{
			GameplaySystem.diceSideThrown = Dice1.DiceNum + Dice2.DiceNum + 2;
			
			// TEST VAR: to declare what value the player will move
			// REMOVE FROM PRODUCTION CODE
			// GameplaySystem.diceSideThrown = 2;

       		GameplaySystem.MovePlayer();
		} 

		Dice1.db_coroutine = false; 
		Dice2.db_coroutine = false;
		coroutine = true;
	}

	
}
