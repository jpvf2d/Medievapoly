using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card  //base class
{
  public string cardName;
  public Card(string CardName)
  {
    cardName = CardName;
  }
}

public class PropertyCard: Card
{
  public string cardN;
  public string propertyName;
  public double rent;
  public double one;
  public double two;
  public double three;
  public double four;
  public double hotel;
  public double fullSet;
  public double purchaseCost;
  public string colorCategory; 

   public PropertyCard(string CardName,string cName, string PropertyName, double Rent, double One, double Two, double Three, double Four, double Hotel, double FullSet, double PurchaseCost, string colorCat) : base(CardName)
   {
     cardN = cName;
     propertyName = PropertyName;
     rent = Rent;
     one = One;
     two = Two;
     three = Three;
     four = Four;
     hotel = Hotel;
     fullSet= FullSet;
     purchaseCost = PurchaseCost;
     colorCategory = colorCat; 
   }

}
public class RailroadCard: Card
{
  public string railroadName;
  public double rent1;
  public double twoRailroad;
  public double threeRailroad;
  public string des;
  public double value;

  public RailroadCard(string CardName, string RailroadName, double Rent1, double TwoRailroad, double ThreeRailroad, string Des, double val) : base(CardName)
  {
    railroadName = RailroadName;
    rent1 = Rent1;
    twoRailroad = TwoRailroad;
    threeRailroad = ThreeRailroad;
    des = Des;
    value = val;
  }
}

public class UtilitiesCard: Card
{
  public string utilitiesName;
  public string rent1;
  public string rent2;
  public double value;

  public UtilitiesCard(string CardName, string UtilitiesName, string Rent1, string Rent2, double val) : base(CardName)
  {
    utilitiesName = UtilitiesName;
    rent1 = Rent1;
    rent2 = Rent2;
    value = val;
  }
}
