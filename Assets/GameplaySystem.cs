using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    
    public static GameObject[] players;
    public static int numPlayers = 2;
    public static int diceSideThrown = 0;
    public static int[] playersStartSpace = {0, 0, 0, 0};
    public static bool gameOver = false;
    private static int turn = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[] {GameObject.Find("Player1"), GameObject.Find("Player2")};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void MovePlayer(int player)
    {
        player--;
        players[player].GetComponent<Player>().boardSpaceIndex = (players[player].GetComponent<Player>().boardSpaceIndex + diceSideThrown) % 40;
        players[player].GetComponent<Player>().move = true;
    }
    
}
