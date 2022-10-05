using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
	
	public Button y_Btn, n_Btn;
	
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
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		GameplaySystem.switchPlayerView = true; 
	}
	
	private void NoPurchase()
	{
		Debug.Log("Did not purchase");
		GameplaySystem.PurchasePropertyMenu.SetActive(false);
		DisplayCard.stopDisplay = true;
		GameplaySystem.switchPlayerView = true; 
	}
	
}