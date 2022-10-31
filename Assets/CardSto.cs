using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Card;
//using Random = System.Random;
public class CardSto : MonoBehaviour
{
    public TextAsset cardData;
    //public TextAsset cardData1;
    //public Image titleImage;
    public static List<Card> newCardList = new List<Card>();
    //public static Random rnd = new Random();
    public Card card;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        //titleColor(propertyName);
    }

    public void LoadData()
    {
      //titleColor();
      string[] dataRow = cardData.text.Split('\n');
      string[] dataRow1 = cardData.text.Split('\n');
      foreach (var row in dataRow)
      {
        string[] rowArray = row.Split(',');
        if(rowArray[0] == "cName")
        {
          continue;
        }
        else if (rowArray[0] == "property")
        {
          //create new card
          string cardN = rowArray[1];
          string propertyName = rowArray[2];
          double rent  = double.Parse(rowArray[3]);
          double one = double.Parse(rowArray[4]);
          double two = double.Parse(rowArray[5]);
          double three = double.Parse(rowArray[6]);
          double four = double.Parse(rowArray[7]);
          double hotel = double.Parse(rowArray[8]);
          double fullSet = double.Parse(rowArray[9]);
          double purchaseCost = double.Parse(rowArray[10]);
          string CardName = "property";
          PropertyCard propertyCard = new PropertyCard(CardName, cardN, propertyName, rent, one, two, three, four, hotel, fullSet, purchaseCost);
          newCardList.Add(propertyCard);
          //Debug.Log(newCardList.Count);
          //Debug.Log(cardN + propertyName + purchaseCost);
          //Debug.Log()

        }
        else if(rowArray[0] == "utility")
        {
          string propertyName = rowArray[1];
          string rent1 = rowArray[2];
          string rent2 = rowArray[3];
          double three = double.Parse(rowArray[4]);
          string cardName = "utility";
          UtilitiesCard utilitiesCard = new UtilitiesCard(cardName, propertyName, rent1, rent2, three);
          newCardList.Add(utilitiesCard);

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
          newCardList.Add(railCard);
          //Debug.Log("new " + railCard.twoRailroad);
          //Debug.Log(newCardList.Count);
        }
      }
      PropertySpace.continueAssignCard = true; 
    }

    public Card rCard(int i)
    {
      //var rnd = new Random();
      //var numbers = Enumerable.Range(0, newCardList.Count).OrderBy(x => rnd.Next()).Take(5).ToList();
      //for (int i = 0; i < newCardList.Count; i++)
      //{
        Card card = newCardList[i];
        return card;
      //}
      //int number = rnd.Next(1, newCardList.Count);
      //Card card = newCardList[numbers];
      //Card card = newCardList[Random.Range(0, newCardList.Count)];
      //return card;
   }


}
