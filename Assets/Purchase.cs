using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
	
	public Button y_Btn, n_Btn;
	public static bool purchasedProp = false; 
	
	void Start()
	{
		Button yesBtn = y_Btn.GetComponent<Button>();
		Button noBtn = n_Btn.GetComponent<Button>();
		yesBtn.onClick.AddListener(PurchaseProp);
		noBtn.onClick.AddListener(NoPurchase);
	}
	private void PurchaseProp()
	{
		Debug.Log("Purchase property");

		purchasedProp = true; 
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		SpaceLogic.continue_sl = true; 
	}
	
	private void NoPurchase()
	{
		Debug.Log("Did not purchase");

		purchasedProp = false; 
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		SpaceLogic.continue_sl = true; 
	}
	
}