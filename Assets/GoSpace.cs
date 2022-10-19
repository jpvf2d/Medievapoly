using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSpace : BoardSpace
{

    public int index;
    [HideInInspector] public static bool passedGo = false; //Set when player passes Go Space 

    private void Update()
    {
        if(passedGo)
        {
            passedGo = false; 
            StartCoroutine("passing");
        }
    }

    public override void passing()
    {
        GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += 200;
    }

    public override void land()
    {
        GameplaySystem.players[GameplaySystem.turn].GetComponent<Player>().money += 200;
        SpaceLogic.continue_sl = true;
    }

    public override void stuck()
    {
        Debug.Log("Do nothing, stuck:GoSpace");
    }



}