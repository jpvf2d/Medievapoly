/*
Script for allowing players to negotiate purchasing 'Get out of Jail Free' cards
More "Jail logic" is in the 'DiceButton.cs' script 
*/

using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JailCardDisplay : MonoBehaviour
{	
    public static GameObject jailCardDisplay; //Initial display (player decides if they want to negotiate)
    public static GameObject jailCardNegotiate; //Player enters amount willing to pay
    public static GameObject jailCardAccept;  //Owner of card accepts/declines offer 
    public GameObject JailCardText; //Text object for jailCardDisplay
    public GameObject JailCardPlayer; //Text object for jailCardDisplay 

	public Button p_btn, n_btn; //Buttons for initial Jail Card display (jailCardDisplay)
    public Button y_btn, n_a_btn;  //Buttons for accepting an offer to purchase jail cards (jailCardAccept)
    public InputField jcn_inpt; //Input field for entering offer price (jailCardNegotiate) 

    public static bool purchaseJailCard = false; 
    public static int jailCardOwner;  
    public static bool activateJailCardDisplay = false; 
    private int negotiatedPrice = 0; 

    public static bool cont_jcd = false; 
    public static bool negOver = false; 
    public static bool noNeg = false; 
    public static bool overBargained = false; 
    
    void Start()
    {
        jailCardDisplay = GameObject.Find("JailCardDisplay"); 
        jailCardDisplay.SetActive(false);

        jailCardNegotiate = GameObject.Find("JailCardNegotiate"); 
        jailCardNegotiate.SetActive(false);

        jailCardAccept = GameObject.Find("JailCardAccept"); 
        jailCardAccept.SetActive(false);

        Button purchaseBtn = p_btn.GetComponent<Button>();
        Button noBtn = n_btn.GetComponent<Button>();
        purchaseBtn.onClick.AddListener(Purchase);
        noBtn.onClick.AddListener(No);

        InputField jcnInput = jcn_inpt.GetComponent<InputField>();  
        jcnInput.onSubmit.AddListener((price) => Negotiate(price));

        Button acceptYesBtn = y_btn.GetComponent<Button>();
        Button acceptNoBtn = n_a_btn.GetComponent<Button>();
        acceptYesBtn.onClick.AddListener(AcceptYes);
        acceptNoBtn.onClick.AddListener(AcceptNo);
    }

    void Update()
    {
        if(activateJailCardDisplay)
        {
            activateJailCardDisplay = false; 
            jailCardDisplay.SetActive(true); 
            JailCardPlayer.GetComponent<TMP_Text>().text = "Player " +  jailCardOwner.ToString();
        }
    }
    private void Purchase() //Player chooses to purchase/negotiate cards from other players 
    {
	    jailCardDisplay.SetActive(false); 
        jailCardNegotiate.SetActive(true); 

    }

    private void No() //Player chooses not to purchase/negotiate cards 
    {
        noNeg = true; 
        jailCardDisplay.SetActive(false); 
        cont_jcd = true; 
    }

    private void Negotiate(string price) //Player enters offer amount for cards 
    {
        negotiatedPrice = int.Parse(price); 

        if(negotiatedPrice >= GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money)
        {
            overBargained = true; //Player cannot offer more than they have 
            jailCardNegotiate.SetActive(false);
            cont_jcd = true; 
        }
        else
        {
            jailCardNegotiate.SetActive(false);
            jailCardAccept.SetActive(true); 
        }
    }

    private void AcceptYes() //Card owner accepts player's offer 
    {
        GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money -= negotiatedPrice; 
        GameplaySystem.players[jailCardOwner].GetComponent<Player>().money += negotiatedPrice;
        JailSpace.availableJailCards[jailCardOwner] = 0; 
        jailCardAccept.SetActive(false); 
        cont_jcd = true; 
        negOver = true; 
    }
 
    private void AcceptNo() //Card owner rejects player's offer 
    {
        jailCardAccept.SetActive(false);   
        cont_jcd = true; 
        Debug.Log("noNeg: " + noNeg);
    }


}
