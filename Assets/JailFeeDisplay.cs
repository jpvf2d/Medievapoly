using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JailFeeDisplay : MonoBehaviour
{
	
	public Button y_Btn, n_Btn;
	public static bool cont_jailfee = false; 
	public static bool paidFee = false;
	public static GameObject payJailFee;

	void Start()
	{
		payJailFee = GameObject.Find("PayJailFee");
		payJailFee.SetActive(false);
		Button yesBtn = y_Btn.GetComponent<Button>();
		Button noBtn = n_Btn.GetComponent<Button>();
		yesBtn.onClick.AddListener(Yes);
		noBtn.onClick.AddListener(No);
	}
	private void Yes()
	{
		StartCoroutine(ActionTextScript.display("Paid fine, escaping the gallows"));
		GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= 50;
		paidFee = true;
		payJailFee.SetActive(false);
		cont_jailfee = true;
	}
	
	private void No()
	{
		StartCoroutine(ActionTextScript.display("Did not pay fine, stuck at the gallows"));
		paidFee = false;
		payJailFee.SetActive(false);
		cont_jailfee = true; 
	}
	
}