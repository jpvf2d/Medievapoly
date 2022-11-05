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
		StartCoroutine(ActionTextScript.display("Purchased property"));
		purchasedProp = true; 
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		SpaceLogic.continue_sl = true; 
	}
	
	private void NoPurchase()
	{
		StartCoroutine(ActionTextScript.display("Did not purchase"));
		purchasedProp = false; 
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		SpaceLogic.continue_sl = true; 
	}

	// Weird workaround for autoplay (public static)
	public static void AutoPurchaseProp()
	{
		purchasedProp = true; 
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		SpaceLogic.continue_sl = true; 
	}
	
}