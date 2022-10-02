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
	private bool cont = false;
	private bool cont2 = false;
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
			StartCoroutine(Dice1.RollTheDice());
			StartCoroutine(Dice2.RollTheDice());
			StartCoroutine(MoveThePlayers());
		}
    }
	
	private IEnumerator MoveThePlayers()
	{
		while(Dice1.db_coroutine == false || Dice2.db_coroutine == false)
		{
			yield return new WaitForSeconds(0.05f);
		}
		
		GameplaySystem.diceSideThrown = Dice1.DiceNum + Dice2.DiceNum;
		if(turn > 2)
            turn = 1;
        GameplaySystem.MovePlayer(turn);
        turn += 1;
		coroutine = true; 
		Dice1.db_coroutine = false; 
		Dice2.db_coroutine = false; 
	}

	
}
