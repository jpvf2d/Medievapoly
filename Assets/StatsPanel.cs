using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] public TextMeshPro money, location;
    [SerializeField] private Player playerStats;
    public Transform[] boardSpaces;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    void SetStats()
    {
        money.text = "$" + playerStats.money.ToString();
        GameObject boardSpacesIndexed = GameObject.Find("BoardSpaces");
        BoardSpace[] boardSpacesArray = boardSpacesIndexed.GetComponent<SpaceLogic>().allBoardSpaces;

        // Displays location
        if(playerStats.isDead){
            location.text = "Dead";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is GoSpace){
            location.text = "Go Space";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is ChanceSpace){
            location.text = "Chance Space";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is ChestSpace){
            location.text = "Community Chest";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is JailSpace){
            location.text = "Gallows";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is LuxTaxSpace){
            location.text = "Luxury Tax";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is TaxSpace){
            location.text = "Tax";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is FreeParking){
            location.text = "Free Parking";
        }
        else if(boardSpacesArray[playerStats.boardSpaceIndex] is PropertySpace){
            if(boardSpacesArray[playerStats.boardSpaceIndex].GetComponent<PropertySpace>().isUtility){
                location.text = (boardSpacesArray[playerStats.boardSpaceIndex].GetComponent<PropertySpace>().propertyCard as UtilitiesCard).utilitiesName;
            }
            else if(boardSpacesArray[playerStats.boardSpaceIndex].GetComponent<PropertySpace>().isRailroad){
                location.text = (boardSpacesArray[playerStats.boardSpaceIndex].GetComponent<PropertySpace>().propertyCard as RailroadCard).railroadName;
            }
            else{
                location.text = (boardSpacesArray[playerStats.boardSpaceIndex].GetComponent<PropertySpace>().propertyCard as PropertyCard).propertyName;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetStats();
    }
}
