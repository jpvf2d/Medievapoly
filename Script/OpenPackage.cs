using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPackage : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject CardShow;
    CardRailroad CardRailroad;
    // Start is called before the first frame update
    void Start()
    {
        CardRailroad = GetComponent<CardRailroad>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void clickOpen()
    {
      //clearCard();

      for (int i = 0; i < 4; i++)
      {
        GameObject newCard = GameObject.Instantiate(cardPrefab, CardShow.transform);
        newCard.GetComponent<CardDisplay>().card = CardRailroad.rCard(i);
        //cards.Add(newCard);
      }
    }

}
