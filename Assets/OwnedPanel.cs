using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OwnedPanel : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    [SerializeField] public TextMeshPro locationL, locationR, money, location;
    public static GameObject[] players;
    private Player playerStats;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[] {GameObject.Find("Player1"), GameObject.Find("Player2"), GameObject.Find("Player3"), GameObject.Find("Player4")};
        SetStats();
    }

    void ChangeSprite()
    {
        switch(GameplaySystem.turn)
        {
            case 0:
                spriteRenderer.sprite = sprite1; 
                break;
            case 1:
                spriteRenderer.sprite = sprite2;
                break;
            case 2:
                spriteRenderer.sprite = sprite3;
                break;
            case 3:
                spriteRenderer.sprite = sprite4;
                break;
        }  
    }

    void SetStats()
    {
        int turn = GameplaySystem.turn;
        playerStats = GameplaySystem.players[turn].GetComponent<Player>();
        string placeHolder = "";
        string displayL = "";
        string displayR = "";
        List<int> properties = GameplaySystem.players[turn].GetComponent<Player>().property;
        money.text = "$" + players[turn].GetComponent<Player>().money.ToString();

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

        // Displays indexs of owned properties in 3 rows (change to names later)        
        for(int i = 0; i < properties.Count; i++){
            if(boardSpacesArray[properties[i]].GetComponent<PropertySpace>().isUtility){
                placeHolder = (boardSpacesArray[properties[i]].GetComponent<PropertySpace>().propertyCard as UtilitiesCard).utilitiesName;
            }
            else if(boardSpacesArray[properties[i]].GetComponent<PropertySpace>().isRailroad){
                placeHolder = (boardSpacesArray[properties[i]].GetComponent<PropertySpace>().propertyCard as RailroadCard).railroadName;
            }
            else{
                placeHolder = (boardSpacesArray[properties[i]].GetComponent<PropertySpace>().propertyCard as PropertyCard).propertyName;
            }
            
            if(i % 2 == 0){
                displayL += placeHolder + "\n";
            }
            else{
                displayR += placeHolder + "\n";
            }
        }
        locationL.text = displayL;
        locationR.text = displayR;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
        SetStats();
    }
}