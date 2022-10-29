using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OwnedPanel : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;

    [SerializeField] public TextMeshPro locationL, locationM, locationR, money, location;
    public static GameObject[] players;

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
        }  
    }

    void SetStats()
    {
        int turn = GameplaySystem.turn;
        string displayL = "";
        string displayM = "";
        string displayR = "";
        List<int> properties = GameplaySystem.players[turn].GetComponent<Player>().property;
        money.text = "$" + players[turn].GetComponent<Player>().money.ToString();
        location.text = players[turn].GetComponent<Player>().boardSpaceIndex.ToString(); // Change to names once cards are in game

        // Displays indexs of owned properties in 3 rows (change to names later)
        for(int i = 0; i < properties.Count; i++){
            if(i % 3 == 0){
                displayL += properties[i] + "\n";
            }
            else if (i % 3 == 1){
                displayM += properties[i] + "\n";
            }
            else{
                displayR += properties[i] + "\n";
            }
        }
        locationL.text = displayL;
        locationM.text = displayM;
        locationR.text = displayR;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
        SetStats();
    }
}