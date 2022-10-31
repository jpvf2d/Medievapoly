/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CardSto;
public class CardDisplay : MonoBehaviour
{
    public Text nameText;
    public Text propertyText;
    public Text rentP;
    public Text oneP;
    public Text twoP;
    public Text threeP;
    public Text fourP;
    public Text hotelP;
    public Text fullsetP;
    public Text purchaseP;
    public Text railroadN;
    public Text railroadRent;
    public Text railroadTwo;
    public Text railroadThree;
    public Text railroadDes;
    public Text railroadValue;

    public Text nameU;
    public Text rentOneU;
    public Text rentTwoU;
    public Text valueU;

    public Image backgroundImage;
    public Image titleImage;
    public Image railroadImage;
    public Image utilityImage;
    public Card card;
    // Start is called before the first frame update
    void Start()
    {
        ShowCard();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowCard()
    {
      if (card is PropertyCard)
      {
        var property = card as PropertyCard;
        nameText.text = property.cardN;
        propertyText.text = property.propertyName;
        rentP.text = "Rent: $" + property.rent.ToString();
        oneP.text = "With 1 House:       $" + property.one.ToString();
        twoP.text = "With 2 Houses:     $" + property.two.ToString();
        threeP.text = "With 3 Houses:     $" + property.three.ToString();
        fourP.text = "With 4 Houses:     $" + property.four.ToString();
        hotelP.text = "With HOTEL:        $" + property.hotel.ToString();
        fullsetP.text = "Fullset: $" + property.fullSet.ToString();
        purchaseP.text = "Purchase Cost: $" + property.purchaseCost.ToString();
        if (propertyText.text == "The Eyrie" || propertyText.text == "The Hornburg" || propertyText.text == "The Hightower")
        {
          titleImage.GetComponent<Image>().color = new Color32(153,204,255,255);
        }
        else if (propertyText.text == "Leeds Castle" || propertyText.text == "Stirling Castle" || propertyText.text == "Tintagel Castle")
        {
          titleImage.GetComponent<Image>().color = new Color32(255,204,104,255);
        }
        else if (propertyText.text == "Carneath Hold" || propertyText.text == "Hewgill Hold" || propertyText.text == "Haarton Hold")
        {
          titleImage.GetComponent<Image>().color = new Color32(234,90,179,255);
        }
        else if (propertyText.text == "Bodiam Castle" || propertyText.text == "Bojnice Castle" || propertyText.text == "Bran Castle")
        {
          titleImage.GetComponent<Image>().color = new Color32(240,115,64,255);
        }
        else if (propertyText.text == "Avolire Castle" || propertyText.text == "Minbury Castle" || propertyText.text == "Nartley Castle")
        {
          titleImage.GetComponent<Image>().color = new Color32(255,63,77,255);
        }
        else if (propertyText.text == "Kilkenny Castle" || propertyText.text == "Kōchi Castle" || propertyText.text == "Komine Castle")
        {
          titleImage.GetComponent<Image>().color = new Color32(0,153,51,255);
        }
        else if (propertyText.text == "Yamagata" || propertyText.text == "Ueda Castle")
        {
          titleImage.GetComponent<Image>().color = new Color32(120,0,210,100);
        }
        else if (propertyText.text == "Shibata Castle" || propertyText.text == "Sakura Castle")
        {
          titleImage.GetComponent<Image>().color = new Color32(33,81,195,200);
        }

        //railroad card stuff
        railroadN.gameObject.SetActive(false);
        railroadRent.gameObject.SetActive(false);
        railroadTwo.gameObject.SetActive(false);
        railroadThree.gameObject.SetActive(false);
        railroadDes.gameObject.SetActive(false);
        railroadValue.gameObject.SetActive(false);
        railroadImage.gameObject.SetActive(false);
        //utilities card stuff
        nameU.gameObject.SetActive(false);
        rentOneU.gameObject.SetActive(false);
        rentTwoU.gameObject.SetActive(false);
        valueU.gameObject.SetActive(false);
        utilityImage.gameObject.SetActive(false);
        //titleImage.image = property.GetComponent<Image>();

        //LoadData();
        //Debug.Log("new" + propertyText.text);
        //titleImage.GetComponent<Image>().color = new Color32(64,128,192,255);
        //Debug.Log("new " + purchaseT.text);
      }
      else if (card is RailroadCard)
      {
        var railroad = card as RailroadCard;
        railroadN.text = railroad.railroadName;
        railroadRent.text = "RENT                     $" + railroad.rent1.ToString();
        railroadTwo.text = "If 2 railroads are owned   $" + railroad.twoRailroad.ToString();
        railroadThree.text = "If 3 railroads are owned $" + railroad.threeRailroad.ToString();
        railroadDes.text = railroad.des;
        railroadValue.text = "Mortgage Value $" + railroad.value.ToString();
        //railroadImage.GetComponent<Image>().sprite
        //property card stuff
        propertyText.gameObject.SetActive(false);
        rentP.gameObject.SetActive(false);
        oneP.gameObject.SetActive(false);
        twoP.gameObject.SetActive(false);
        threeP.gameObject.SetActive(false);
        fourP.gameObject.SetActive(false);
        hotelP.gameObject.SetActive(false);
        fullsetP.gameObject.SetActive(false);
        purchaseP.gameObject.SetActive(false);
        titleImage.gameObject.SetActive(false);

        //utilities card stuff
        nameU.gameObject.SetActive(false);
        rentOneU.gameObject.SetActive(false);
        rentTwoU.gameObject.SetActive(false);
        valueU.gameObject.SetActive(false);
        utilityImage.gameObject.SetActive(false);
      }
      else if (card is UtilitiesCard)
      {
        var utility = card as UtilitiesCard;
        nameU.text = utility.utilitiesName;
        rentOneU.text = utility.rent1;
        rentTwoU.text = utility.rent2;
        valueU.text = "Mortgage Value $" + utility.value.ToString();
        Debug.Log("new ");
        Debug.Log("old " + valueU.text);

        //property stuff
        propertyText.gameObject.SetActive(false);
        rentP.gameObject.SetActive(false);
        oneP.gameObject.SetActive(false);
        twoP.gameObject.SetActive(false);
        threeP.gameObject.SetActive(false);
        fourP.gameObject.SetActive(false);
        hotelP.gameObject.SetActive(false);
        fullsetP.gameObject.SetActive(false);
        purchaseP.gameObject.SetActive(false);
        titleImage.gameObject.SetActive(false);

        //railroad stuff
        railroadN.gameObject.SetActive(false);
        railroadRent.gameObject.SetActive(false);
        railroadTwo.gameObject.SetActive(false);
        railroadThree.gameObject.SetActive(false);
        railroadDes.gameObject.SetActive(false);
        railroadValue.gameObject.SetActive(false);
        railroadImage.gameObject.SetActive(false);
      }

    }
}

*/
