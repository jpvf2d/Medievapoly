using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLogic : MonoBehaviour
{

    public static bool coroutine_sl = false; 
    public static bool continue_sl = false; 
    public static bool cont_changeTurns = false; 
    public BoardSpace[] allBoardSpaces; //Array of all board spaces (property, chance, tax, etc.)

    void Update()
    {

        //Start "interactWithSpace" coroutine if player has finished moving 
        if(coroutine_sl) 
        {
            coroutine_sl = false; 
            int spaceIndex = GameplaySystem.playerIndex;
            allBoardSpaces[spaceIndex].land();
            StartCoroutine("interactWithSpace", spaceIndex);
        }   

    }

    private IEnumerator interactWithSpace(int spaceIndex)
    {
        //Wait for ".land()" to finish 
        while(!continue_sl)
        {
            yield return new WaitForSeconds(0.01f);
        }

        continue_sl = false;

        if(Purchase.purchasedProp)
        {
            int turn = GameplaySystem.turn;
            allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().owned = true; 
            allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().indexOfOwner = turn; 
            GameplaySystem.players[turn].GetComponent<Player>().property.Add(spaceIndex);

            float costToPurchaseProperty = allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().TMP_purchaseCost;
            GameplaySystem.players[turn].GetComponent<Player>().money -= costToPurchaseProperty;

            Purchase.purchasedProp = false; 
        }

        // Last thing to occur in order to continue gameplay (turns)
        cont_changeTurns = true; 
    }
}