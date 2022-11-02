using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertySpace : BoardSpace
{
	public int index;
	public string propertyName; 
	[HideInInspector] public bool owned = false;
	[HideInInspector] public int indexOfOwner;

	//Special property cards 
	public bool isRailroad;
	public bool isUtility;


	private float purchaseCost = 0; 
	public Card propertyCard = null; 

	public static bool continueAssignCard = false;

	void Start()
	{
		StartCoroutine("AssignCard");		
	}
	
    public override void passing()
	{
		Debug.Log("Do nothing (PropertySpace:passing)");
	}
	
    public override void land()
	{
			if(!owned)
			{
				if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money >= this.purchaseCost)
				{
					DisplayCard.cardIdx = index;
					DisplayCard.coroutine = true;
					GameplaySystem.PurchasePropertyMenu.SetActive(true);
				}
				else	
				{
					StartCoroutine(ActionTextScript.display("You don't have enough money to purchase this"));
					Debug.Log("Price: " + this.purchaseCost + " Money: " + GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money);
					SpaceLogic.continue_sl = true; 
				}
			}
			
			else
			{

					if(GameplaySystem.turn != indexOfOwner)
					{
						float rent = 0; 
						
						if(isRailroad)
						{
							rent = (float) (this.propertyCard as RailroadCard).rent1;
						}

						else if(isUtility)
						{
							GameObject dice1, dice2;
							dice1 = GameObject.Find("Dice1");
							dice2 = GameObject.Find("Dice2");
							int diceVal = dice1.GetComponent<Dice>().DiceNum + dice2.GetComponent<Dice>().DiceNum + 2;
							int multiplyer = 4;
							if(GameplaySystem.players[indexOfOwner].GetComponent<Player>().numUtilities == 2)
							{
								multiplyer = 10;
							}
							rent = (float) diceVal * multiplyer;
						}

						else
							rent = (float) (this.propertyCard as PropertyCard).rent;


						if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money < rent)
						{
							float lastOfMoney = GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money;
							GameplaySystem.players[indexOfOwner].GetComponent<Player>().money += lastOfMoney;
							GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money = -1; 
							StartCoroutine(ActionTextScript.display("Player "+ GameplaySystem.turn + " paid Player " + indexOfOwner + " $" + lastOfMoney + ". You're out of funds!"));
							//TODO: If player runs out of money, their game ends 
							// The below isn't needed because it's already handled in the player file
							// GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().justLost = true;
						}

						else
						{
							StartCoroutine(ActionTextScript.display("Player "+ GameplaySystem.turn + " paid Player " + indexOfOwner + " $" + rent));
							GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= rent;
							GameplaySystem.players[indexOfOwner].GetComponent<Player>().money += rent;
						}
					}

					else
					{
						//TODO: Display graphic saying player owns property
						StartCoroutine(ActionTextScript.display("Player "+ GameplaySystem.turn + " owns this property"));
					}
				
					SpaceLogic.continue_sl = true; 
			}
	}
	
    public override void stuck()
	{
		Debug.Log("Do nothing (PropertySpace:stuck");
	}

	private IEnumerator AssignCard()
	{
		while(!continueAssignCard)
		{
			yield return new WaitForSeconds(0.05f);
		}

	
		if(isRailroad)
		{
			for(int i = 0; i < CardSto.newCardList.Count; i++)
			{
				if (CardSto.newCardList[i] is RailroadCard)
				{
					if((CardSto.newCardList[i] as RailroadCard).railroadName == this.propertyName)
					{	
						this.propertyCard = CardSto.newCardList[i]; 
						this.purchaseCost = (float) (this.propertyCard as RailroadCard).value; 
					}
				}
			}

			Debug.Log("Railroad: " + (this.propertyCard as RailroadCard).railroadName);
		}


		else if(isUtility)
		{
			for(int i = 0; i < CardSto.newCardList.Count; i++)
			{
				if (CardSto.newCardList[i] is UtilitiesCard)
				{
					if((CardSto.newCardList[i] as UtilitiesCard).utilitiesName == this.propertyName)
					{	
						this.propertyCard = CardSto.newCardList[i]; 
						this.purchaseCost = (float) (this.propertyCard as UtilitiesCard).value; 
					}
				}
			}

			Debug.Log("Utility: " + (this.propertyCard as UtilitiesCard).utilitiesName); 
		}

		else
		{
			for(int i = 0; i < CardSto.newCardList.Count; i++)
			{
				if(CardSto.newCardList[i] is PropertyCard)
				{
					if((CardSto.newCardList[i] as PropertyCard).propertyName == this.propertyName)
					{
						this.propertyCard = CardSto.newCardList[i]; 
						this.purchaseCost = (float) (this.propertyCard as PropertyCard).purchaseCost; 
					}
				}
			}
			Debug.Log("Property: " + (this.propertyCard as PropertyCard).propertyName);
		}

	}
	
}
