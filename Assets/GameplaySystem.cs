using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameplaySystem : MonoBehaviour
{
    
    public static GameObject[] players;
    public static int numPlayers = 2;
    public static int diceSideThrown = 0;
    public static int[] playersStartSpace = {0, 0, 0, 0};
    public static bool gameOver = false;
    public static int turn = 0;
    public static GameObject dice1,dice2;

    public class StaticMB: MonoBehaviour { }
    private static StaticMB mb;

    public static GameObject[] cams;
    public static GameObject activeCam = null;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[] {GameObject.Find("Player1"), GameObject.Find("Player2")};
        cams = new GameObject[] {GameObject.Find("CM vcam1"), GameObject.Find("CM vcam2")};
        dice1 = GameObject.Find("Dice1");
		dice2 = GameObject.Find("Dice2");
         if (mb == null)
        {
            GameObject gameObject = new GameObject("MyStatic");
            mb = gameObject.AddComponent<StaticMB>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlayerTurn()
    {
        
    }

    public static void MovePlayer()
    {
        players[turn].GetComponent<Player>().boardSpaceIndex = (players[turn].GetComponent<Player>().boardSpaceIndex + diceSideThrown) % 40;
        players[turn].GetComponent<Player>().move = true;
        turn += 1;
        if(turn >= numPlayers)
            turn = 0;
        mb.StartCoroutine(SwitchCamera(cams[turn]));
    }

    public static void roll()
    {
		Debug.Log("rolling");
        diceSideThrown = Random.Range(0,6) + 1;
        //mb.StartCoroutine(dice.GetComponent<Dice>().RollTheDice(diceSideThrown - 1));
        mb.StartCoroutine(dice1.GetComponent<Dice>().RollTheDice(diceSideThrown - 1));
		mb.StartCoroutine(dice2.GetComponent<Dice>().RollTheDice(diceSideThrown - 1));
    }

    public static IEnumerator SwitchCamera(GameObject cam)
    {
        yield return new WaitForSeconds(2.0f);
        cam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        activeCam = cam;
        foreach(GameObject c in cams)
        {
            if(c.GetComponent<CinemachineVirtualCamera>() != activeCam.GetComponent<CinemachineVirtualCamera>())
                c.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
    }
}
