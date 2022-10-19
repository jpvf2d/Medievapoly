using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailSpace : BoardSpace
{

    public int index;
    [HideInInspector] public static int[] playersInJail = new int[4] {-1,-1,-1,-1}; 
    [HideInInspector] public static bool waitInJail = false; 


    private void Update()
    {
        //If player stuck in jail, determine how much longer player stuck
        if(waitInJail)
        {
            waitInJail = false; 
            StartCoroutine("stuck");
            StartCoroutine(GameplaySystem.ChangeTurns());
        }

    }
    public override void passing()
    {
        Debug.Log("Do nothing, land:JailSpace");
    }

    public override void land()
    {
        StartCoroutine(ActionTextScript.display("Visting jail..."));
        SpaceLogic.continue_sl = true;  
    }

    public override void stuck()
    {
        int turn = GameplaySystem.turn;
        if(playersInJail[turn] >= 2) // Release player after 3 turns
        {
            GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().stuckInPlace = false;
            playersInJail[turn] = -1;
        }

        else if(playersInJail[turn] > -1)
        {
            playersInJail[turn] += 1; 
        }
        SpaceLogic.cont_changeTurns = true; 
    }

}