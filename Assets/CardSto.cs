using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSto : MonoBehaviour
{
    public TextAsset cardData;
    public static List<PropertyCard> cardList = new List<PropertyCard>();
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
      string[] dataRow = cardData.text.Split('\n');
      foreach (var row in dataRow)
      {
        string[] rowArray = row.Split(',');
        if(rowArray[0] == "Property Name")
        {
          continue;
        }
        else if (rowArray[0] == "property")
        {
          string propertyName = rowArray[1];
          double rent  = double.Parse(rowArray[2]);
          double one = double.Parse(rowArray[3]);
          double two = double.Parse(rowArray[4]);
          double three = double.Parse(rowArray[5]);
          double four = double.Parse(rowArray[6]);
          double hotel = double.Parse(rowArray[7]);
          double fullSet = double.Parse(rowArray[8]);
          double purchaseCost = double.Parse(rowArray[9]);
          string cardName = "property";
          PropertyCard propertyCard = new PropertyCard(cardName, propertyName, rent, one, two, three, four, hotel, fullSet, purchaseCost);
          cardList.Add(propertyCard);
        }
      }

      PropertySpace.continueAssignCard_cs = true; 
    }
    public Card rCard()
    {
      Card card = cardList[Random.Range(0, cardList.Count)];
      return card;
    }

}
