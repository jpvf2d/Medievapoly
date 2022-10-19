using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceButton : MonoBehaviour
{
	private SpriteRenderer rend;
	private Sprite[] diceSides;
	public Dice Dice1;
	public Dice Dice2;
	private int turn = 1; 
	private bool coroutine = true;
	
    // Start is called before the first frame update
    void Start()
    {
		 rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceButton/");
        rend.sprite = diceSides[8];
		
    }

       private void OnMouseDown() 
    {
		if(coroutine)
		{
			coroutine = false;
			GameplaySystem.roll();
			StartCoroutine(MoveThePlayers());
		}
    }
	
	private IEnumerator MoveThePlayers()
	{
		while(Dice1.db_coroutine == false || Dice2.db_coroutine == false)
		{
			yield return new WaitForSeconds(0.05f);
		}
		
		if(GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().stuckInPlace == true)
		{
			JailSpace.waitInJail = true; 
			StartCoroutine(ActionTextScript.display("Player: " + GameplaySystem.turn + " is stuck in jail"));
			//TODO: Roll doubles allows player to leave jail 
		}

		else
		{
			GameplaySystem.diceSideThrown = Dice1.DiceNum + Dice2.DiceNum + 2;
       		GameplaySystem.MovePlayer();
		} 

		Dice1.db_coroutine = false; 
		Dice2.db_coroutine = false;
		coroutine = true;
	}

	
}
