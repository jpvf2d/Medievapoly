using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToJailSpace : BoardSpace
{

    public int index;
    [HideInInspector] public static bool sentToJail = false;
    [HideInInspector] public static int pIdx = 0;
    
    public override void passing()
    {
        Debug.Log("Do nothing, passing:GoToJailSpace");
    }

    public override void land()
    {
        Debug.Log("Player is going to jail!");
        pIdx = GameplaySystem.turn; //player index (for JailSpace.playersInJail array)
        
        sentToJail = true; // Use in Player.Move to determine if player should interact with space
        StartCoroutine(throwInJail(pIdx));

        // Move player to jail
        GameplaySystem.players[pIdx].GetComponent<Player>().boardSpaceIndex = 10; 
        GameplaySystem.playerIndex = GameplaySystem.players[pIdx].GetComponent<Player>().boardSpaceIndex;
        GameplaySystem.players[pIdx].GetComponent<Player>().move = true;
        
    }

    private IEnumerator throwInJail(int pIdx)
    {
        while(sentToJail)
        {
            yield return new WaitForSeconds(0.01f);
        }

        JailSpace.playersInJail[pIdx] = 0;
        GameplaySystem.players[pIdx].GetComponent<Player>().stuckInPlace = true;
        SpaceLogic.continue_sl = true; 
    }

    public override void stuck()
    {
        Debug.Log("Do nothing, stuck:GoToJailSpace");
    }
}