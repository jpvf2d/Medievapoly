using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    
    //private static GameObject whoWinsTextShadow, player1MoveText, player2MoveText;
    private static GameObject player1, player2;
    public static int diceSideThrown = 0;
    public static int player1StartSpace = 0;
    public static int player2StartSpace = 0;
    public static bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //whoWinsTextShadow = GameObject.Find("WhoWinsText");
        //player1MoveText = GameObject.Find("Player1MoveText");
        //player2MoveText = GameObject.Find("Player2MoveText");

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<Player>().move = false;
        player2.GetComponent<Player>().move = false;

        //whoWinsTextShadow.GameObject.SetActive(false);
        //player1MoveText.GameObject.SetActive(false);
        //player2MoveText.GameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player1.GetComponent<Player>().boardSpaceIndex > player1StartSpace + diceSideThrown)
        {
            player1.GetComponent<Player>().move = false;
            //player1MoveText.GameObject.SetActive(false);
            //player1MoveText.GameObject.SetActive(true);
            if(player1.GetComponent<Player>().boardSpaceIndex != 0)
                player1StartSpace = --player1.GetComponent<Player>().boardSpaceIndex;
        }
        if(player2.GetComponent<Player>().boardSpaceIndex > player2StartSpace + diceSideThrown)
        {
            player2.GetComponent<Player>().move = false;
            //player2MoveText.GameObject.SetActive(false);
            //player2MoveText.GameObject.SetActive(true);
            if(player2.GetComponent<Player>().boardSpaceIndex != 0)
                player2StartSpace = --player2.GetComponent<Player>().boardSpaceIndex;
        }
    }

    public static void MovePlayer(int player)
    {
        switch(player)
        {
            case 1:
                player1.GetComponent<Player>().boardSpaceIndex = (player1.GetComponent<Player>().boardSpaceIndex + diceSideThrown) % 40;
                if(player1StartSpace > player1.GetComponent<Player>().boardSpaceIndex)
                    player1StartSpace = -1;
                player1.GetComponent<Player>().move = true;
                break;
            case 2:
                player2.GetComponent<Player>().boardSpaceIndex = (player2.GetComponent<Player>().boardSpaceIndex + diceSideThrown) % 40;
                if(player2StartSpace > player2.GetComponent<Player>().boardSpaceIndex)
                    player2StartSpace = -1;
                player2.GetComponent<Player>().move = true;
                break;
        }
    }
    
}
