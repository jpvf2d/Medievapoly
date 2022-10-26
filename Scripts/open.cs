using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;
using static CardDisplay;
public class open : MonoBehaviour
{
    public GameObject cPrefabs;
    public GameObject cardPull;
    CardSto CardSto;
    List<GameObject> cards = new List<GameObject>();
    public Card card;
    // Start is called before the first frame update
    void Start()
    {
        CardSto = GetComponent<CardSto>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickOpen()
    {
/*
      clearCard();
        for (int i = 0; i < 28; i++)
        {
          GameObject newCard = GameObject.Instantiate(cPrefabs, cardPull.transform);
          newCard.GetComponent<CardDisplay>().card = CardSto.rCard(i);
          cards.Add(newCard);
        }
        */
        GameObject newCard = GameObject.Instantiate(cPrefabs, cardPull.transform);
        newCard.GetComponent<CardDisplay>().card = CardSto.rCard(1);
        cards.Add(newCard);

    }
    //clear the cards
    public void clearCard()
    {
      foreach (var card in cards)
      {
        Destroy(card);
      }
      cards.Clear();
    }
}
