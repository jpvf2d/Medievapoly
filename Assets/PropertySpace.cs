using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertySpace : BoardSpace
{
	public  float index;
	public static bool runCoroutine = false;
	private bool owned = false; 
	public float purchaseCost;
	public float[] rentCosts; //Base rent, w/houses (1,2,3,4), and w/hotel
	
	void Update()
	{
		if(runCoroutine)
		{
			if(index == GameplaySystem.playerIndex)
			{
				Debug.Log("index: " + index);
				Debug.Log("GSpi: " + GameplaySystem.playerIndex);
				StartCoroutine("land");
			}
		}
	}
    public override void passing()
	{
		Debug.Log("Do nothing (PropertySpace:passing)");
	}
	
    public override void land()
	{
			Debug.Log("Landed! in Func");
			if(!owned)
			{
				Debug.Log("Offer player ability to purchase");
				DisplayCard.coroutine = true;
				GameplaySystem.PurchasePropertyMenu.SetActive(true);
			}
			
			else
			{
				Debug.Log("Make player pay rent");
			}
			runCoroutine = false; 
	}
	
    public override void stuck()
	{
		Debug.Log("Do nothing (PropertySpace:stuck");
	}
	
}
