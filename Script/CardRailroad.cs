using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRailroad : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList = new List<Card>();
    public Card card;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadData()
    {
      //titleColor();
      string[] dataRow = cardData.text.Split('\n');
      foreach (var row in dataRow)
      {
        string[] rowArray = row.Split(',');
        if(rowArray[0] == "cName")
        {
          continue;
        }
        else if(rowArray[0] == "railroad")
        {
          string railroadName = rowArray[1];
          double rent1 = double.Parse(rowArray[2]);
          double twoRailroad = double.Parse(rowArray[3]);
          double threeRailroad = double.Parse(rowArray[4]);
          string des = rowArray[5];
          double value = double.Parse(rowArray[6]);
          string cardName = "railroad";
          RailroadCard railCard = new RailroadCard(cardName, railroadName, rent1, twoRailroad, threeRailroad, des, value);
          cardList.Add(railCard);
          //Debug.Log("new " + railCard.twoRailroad);
        }
      }

    }
    public Card rCard(int i)
    {

        Card card = cardList[i];
        return card;

   }

}
