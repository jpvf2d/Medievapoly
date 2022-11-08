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
			//Purchase purchase = new Purchase();

			if(!owned)
			{
				if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money >= this.purchaseCost)
				{
					DisplayCard.cardIdx = index;
					if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().autoPlayEnabled == false){
						DisplayCard.coroutine = true;
						GameplaySystem.PurchasePropertyMenu.SetActive(true);
					}
					else{
						Purchase.AutoPurchaseProp();
						Debug.Log("Automatically Purchased Property");
					}
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
							//Rent increases based on how many railroads are owned by owner 
							switch(GameplaySystem.players[indexOfOwner].GetComponent<Player>().numRailroads)
							{
							case 1:
								rent = (float) (this.propertyCard as RailroadCard).rent1;
								break;
							case 2:
								rent = (float) (this.propertyCard as RailroadCard).twoRailroad;
								break;
							case 3:		
								rent = (float) (this.propertyCard as RailroadCard).threeRailroad;
								break;
							case 4:
								rent = (float) (this.propertyCard as RailroadCard).value;
								break;
							}
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
						{
							string color = (this.propertyCard as PropertyCard).colorCategory.Trim();
							int multiplier = GameplaySystem.players[indexOfOwner].GetComponent<Player>().fullSetTracker[color];
						
							if(multiplier == 3)
								multiplier = 2;
							else if(multiplier == 2 && (color == "Blue"|| color == "Purple"))
								multiplier = 2; 
							else
								multiplier = 1; 

							rent = (float) (this.propertyCard as PropertyCard).rent * multiplier;
						} 

						if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money < rent)
						{
							float lastOfMoney = GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money;
							GameplaySystem.players[indexOfOwner].GetComponent<Player>().money += lastOfMoney;
							GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money = -1; 
							StartCoroutine(ActionTextScript.display("Player "+ GameplaySystem.turn + " paid Player " + indexOfOwner + " $" + lastOfMoney + ". You're out of funds!"));
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

			//Debug.Log("Railroad: " + (this.propertyCard as RailroadCard).railroadName);
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

			//Debug.Log("Utility: " + (this.propertyCard as UtilitiesCard).utilitiesName); 
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
			//Debug.Log("Property: " + (this.propertyCard as PropertyCard).propertyName);
		}

	}
	
}
