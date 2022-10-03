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
    public static int turn = 0;
    public static GameObject dice;

    public class StaticMB: MonoBehaviour { }
    private static StaticMB mb;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[] {GameObject.Find("Player1"), GameObject.Find("Player2")};
        dice = GameObject.Find("Dice");
         if (mb == null)
        {
            GameObject gameObject = new GameObject("MyStatic");
            mb = gameObject.AddComponent<StaticMB>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turn >= numPlayers)
            turn = 0;
    }

    public static void MovePlayer()
    {
        players[turn].GetComponent<Player>().boardSpaceIndex = (players[turn].GetComponent<Player>().boardSpaceIndex + diceSideThrown) % 40;
        players[turn].GetComponent<Player>().move = true;
        turn += 1;
    }

    public static void roll()
    {
        diceSideThrown = Random.Range(0,6) + 1;
        mb.StartCoroutine(dice.GetComponent<Dice>().RollTheDice(diceSideThrown - 1));
    }

}
