using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    public Text nameT;
    public Text rentT;
    public Text twoT;
    public Text threeT;
    public Text desT;
    public Text valueT;

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
      if (card is RailroadCard)
      {
        var railroad = card as RailroadCard;
        nameT.text = railroad.railroadName;
        rentT.text = "RENT                                  $" + railroad.rent1.ToString();
        twoT.text = "If 2 railroads are owned  $" + railroad.twoRailroad.ToString();
        threeT.text = "If 3 railroads are owned $" + railroad.threeRailroad.ToString();
        desT.text = railroad.des;
        valueT.text = "Mortgage Value $" + railroad.value.ToString();

      }
    }
}
