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

	//TEMPORARY
	public bool isRailroad;
	public bool isUtility;
	//****TEMPORARY VARIABLES (TMP_*): Delete when Card class is being used ****
	public float TMP_rent = 0; 
	public float TMP_purchaseCost = 0;
	public PropertyCard propertyCard; 
	public RailroadCard railroadCard; 

	public static bool continueAssignCard_cs = false; 
	public static bool continueAssignCard_rr = false;
	/****
	//TODO: Each PropertySpace needs a PropertyCard object associated with it to pull purchase cost and rent information 

	private PropertyCard propertyCard;
	****/

	/*void Update()
	{
		if(runCoroutine)
		{
			if(index == GameplaySystem.playerIndex)
			{
				StartCoroutine("land");
			}
		}
	}*/
	void Start()
	{

			TMP_rent = 500;
			TMP_purchaseCost = 100;
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
				if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money >= TMP_purchaseCost)
				{
					DisplayCard.cardIdx = index;
					DisplayCard.coroutine = true;
					GameplaySystem.PurchasePropertyMenu.SetActive(true);
				}
				else	
				{
					StartCoroutine(ActionTextScript.display("You don't have enough money to purchase this"));
					SpaceLogic.continue_sl = true; 
				}
			}
			
			else
			{

					if(GameplaySystem.turn != indexOfOwner)
					{
						if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money < TMP_rent)
						{
							float lastOfMoney = GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money;
							GameplaySystem.players[indexOfOwner].GetComponent<Player>().money += lastOfMoney;
							GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money = 0; 
							StartCoroutine(ActionTextScript.display("Player "+ GameplaySystem.turn + " paid Player " + indexOfOwner + " $" + lastOfMoney + ". You're out of funds!"));
							//TODO: If player runs out of money, their game ends 
						
						}

						else
						{
							StartCoroutine(ActionTextScript.display("Player "+ GameplaySystem.turn + " paid Player " + indexOfOwner + " $" + TMP_rent));
							GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= TMP_rent;
							GameplaySystem.players[indexOfOwner].GetComponent<Player>().money += TMP_rent;
						}
						/****
						//TODO: Each PropertySpace needs a PropertyCard object associated with it to pull purchase cost and rent information 
					
						GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += propertyCard.rent;
						****/
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
		while(!continueAssignCard_rr || !continueAssignCard_cs)
		{
			yield return new WaitForSeconds(0.05f);
		}

		if(isRailroad)
			{
				for(int i = 0; i < CardRailroad.cardList.Count; i++)
				{
					if(CardRailroad.cardList[i].railroadName == this.propertyName)
					{	
						this.railroadCard = CardRailroad.cardList[i]; 
					}
				}

				Debug.Log("Railroad: " + this.railroadCard.railroadName);
			}

			else if(isUtility)
			{
				TMP_purchaseCost = 100;
				TMP_rent = 500; 
			}

			else
			{
				for(int i = 0; i < CardSto.cardList.Count; i++)
				{
					if(CardSto.cardList[i].propertyName == this.propertyName)
					{
						this.propertyCard = CardSto.cardList[i]; 
					}
				}
				Debug.Log("Property: " + this.propertyCard.propertyName);

			}

	}
	
}
