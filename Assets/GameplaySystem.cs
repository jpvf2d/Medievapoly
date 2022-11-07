using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameplaySystem : MonoBehaviour
{
    
    public static GameObject[] players;
    public static int numPlayers = 4;
    public static int alivePlayers = 4;
    public static int diceSideThrown = 0;
    public static int[] playersStartSpace = {0, 0, 0, 0};
    public static bool gameOver = false;
    public static int turn = 0;
    public static GameObject dice1,dice2;
	public static int playerIndex = 0;
	public static GameObject PurchasePropertyMenu;
	public static GameObject ChanceCard;
	public static GameObject ChestCard;
    public static GameObject TaxCard;
	
    public class StaticMB: MonoBehaviour { }
    private static StaticMB mb;

    public static GameObject[] cams;
    public static GameObject freeCam;
    public static GameObject freeCamText;
    private static bool freeCamActive = false;
    public static GameObject activeCam = null;
    public static GameObject followMe;

    public bool needRoll;
    public int lastPlayer = -1;
    public static GameObject rollButton;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[] {GameObject.Find("Player1"), GameObject.Find("Player2"), GameObject.Find("Player3"), GameObject.Find("Player4")};
        cams = new GameObject[] {GameObject.Find("CM vcam1"), GameObject.Find("CM vcam2"), GameObject.Find("CM vcam3"), GameObject.Find("CM vcam4")};
        activeCam = cams[0];
        followMe = GameObject.Find("FollowMe");
        freeCamText = GameObject.Find("FreeCamText");
        freeCamText.gameObject.SetActive(false);
        freeCam = GameObject.Find("CM vcam free");
        dice1 = GameObject.Find("Dice1");
		dice2 = GameObject.Find("Dice2");
        rollButton = GameObject.Find("DiceButton");
		PurchasePropertyMenu = GameObject.Find("PurchasePropertyMenu");
		ChanceCard = GameObject.Find("ChanceCard");
		ChestCard = GameObject.Find("ChestCard");
        TaxCard = GameObject.Find("TaxCard");
		PurchasePropertyMenu.SetActive(false);
		ChanceCard.SetActive(false);
		ChestCard.SetActive(false);
        TaxCard.SetActive(false);
         if (mb == null)
        {
            GameObject gameObject = new GameObject("MyStatic");
            mb = gameObject.AddComponent<StaticMB>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(freeCamActive)
            {
                freeCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                activeCam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                freeCamActive = false;
                freeCamText.gameObject.SetActive(false);
            }
            else
            {
                followMe.transform.position = new Vector3(players[turn].transform.position.x, 0, 0);
                freeCam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
                activeCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                freeCamActive = true;
                freeCamText.gameObject.SetActive(true);
            }
        }

        if(lastPlayer != turn) {
            needRoll = true;
            lastPlayer = turn;
        }

        if(players[turn].GetComponent<Player>().autoPlayEnabled && this.needRoll) {
            this.needRoll = false;
            AutoRoll();
        }
    }

    public void AutoRoll() {
        rollButton.GetComponent<DiceButton>().Roll();
    }

    public static void PlayerTurn()
    {
        
    }

    public static void MovePlayer()
    {
        if(players[turn].GetComponent<Player>().boardSpaceIndex + diceSideThrown > 40)
            GoSpace.passedGo = true; 

        players[turn].GetComponent<Player>().boardSpaceIndex = (players[turn].GetComponent<Player>().boardSpaceIndex + diceSideThrown) % 40;          
        
        players[turn].GetComponent<Player>().move = true;
		playerIndex = players[turn].GetComponent<Player>().boardSpaceIndex;
        mb.StartCoroutine(ChangeTurns());
    }

    public static void roll()
    {
        diceSideThrown = Random.Range(0,6) + 1;
        mb.StartCoroutine(dice1.GetComponent<Dice>().RollTheDice(diceSideThrown - 1));
		diceSideThrown = Random.Range(0,6) + 1;
		mb.StartCoroutine(dice2.GetComponent<Dice>().RollTheDice(diceSideThrown - 1));
    }

    // Coroutine to keep player turn and camera from changing while player is still interacting with a space
    public static IEnumerator ChangeTurns()
    {
        while(!SpaceLogic.cont_changeTurns)
        {
            yield return new WaitForSeconds(0.01f);
        }


        // Skips dead players
        do{
            turn += 1;

            if(turn >= numPlayers)
                turn = 0;
        }
        while(players[turn].GetComponent<Player>().isDead);

        // Declared winner
        if(GameplaySystem.alivePlayers == 1){
            players[turn].GetComponent<Player>().justWon = true;
        }

        SpaceLogic.cont_changeTurns = false; 
        mb.StartCoroutine(SwitchCamera(cams[turn]));
      
    }

    public static IEnumerator SwitchCamera(GameObject cam)
    {
        yield return new WaitForSeconds(1.0f);
        cam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        activeCam = cam;
        foreach(GameObject c in cams)
        {
            if(c.GetComponent<CinemachineVirtualCamera>() != activeCam.GetComponent<CinemachineVirtualCamera>())
                c.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
    }

}
