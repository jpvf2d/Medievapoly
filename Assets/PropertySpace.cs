using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertySpace : BoardSpace
{
	public int index;
	public static bool runCoroutine = false;
	public float purchaseCost;
	public bool owned = false;
	//public float[] rentCosts; //Base rent, w/houses (1,2,3,4), and w/hotel
	public int indexOfOwner;

	//****TEMPORARY VARIABLES (TMP_*): Delete when Card class is being used ****
	public float TMP_rent = 0; 
	public float TMP_purchaseCost = 0;

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
	}
    public override void passing()
	{
		Debug.Log("Do nothing (PropertySpace:passing)");
	}
	
    public override void land()
	{
			if(!owned)
			{
				Debug.Log("Offer player ability to purchase");
				DisplayCard.coroutine = true;
				GameplaySystem.PurchasePropertyMenu.SetActive(true);
			}
			
			else
			{

					if(GameplaySystem.turn != indexOfOwner)
					{
						//TODO: Display graphic saying how much playerX paid playerY
						Debug.Log("Player "+ GameplaySystem.turn + "paid Player " + indexOfOwner);
						GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= TMP_rent;
						GameplaySystem.players[indexOfOwner].GetComponent<Player>().money += TMP_rent;

						/****
						//TODO: Each PropertySpace needs a PropertyCard object associated with it to pull purchase cost and rent information 
					
						GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += propertyCard.rent;
						****/
					}

					else
					{
						//TODO: Display graphic saying player owns property
						Debug.Log("Player owns this property");
					}
				

					SpaceLogic.continue_sl = true; 
			}
			runCoroutine = false; 
	}
	
    public override void stuck()
	{
		Debug.Log("Do nothing (PropertySpace:stuck");
	}
	
}
