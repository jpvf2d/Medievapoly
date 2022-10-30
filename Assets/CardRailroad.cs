/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRailroad : MonoBehaviour
{
    public TextAsset cardData;
    public static List<RailroadCard> cardList = new List<RailroadCard>();
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
          double purchaseCost = double.Parse(rowArray[7]);
          string cardName = "railroad";
          RailroadCard railCard = new RailroadCard(cardName, railroadName, rent1, twoRailroad, threeRailroad, des, value, purchaseCost);
          cardList.Add(railCard);
        }
      }

      PropertySpace.continueAssignCard_rr = true; 
    }
    public Card rCard(int i)
    {

        Card card = cardList[i];
        return card;

   }

}
*/