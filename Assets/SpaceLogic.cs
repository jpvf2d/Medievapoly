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

            // don't run land function if player was relocated
            bool noRelocation = true;
            for(int i = 0; i < GameplaySystem.numPlayers; i++) {
                if(GameplaySystem.players[i].GetComponent<Player>().relocated) {
                    noRelocation = false;
                    GameplaySystem.players[i].GetComponent<Player>().relocated = false;
                }
            }
            if(noRelocation) {
                allBoardSpaces[spaceIndex].land();
                StartCoroutine("interactWithSpace", spaceIndex);
            }
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
            Purchase.purchasedProp = false; 
            int turn = GameplaySystem.turn;
            allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().owned = true; 
            allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().indexOfOwner = turn; 
            GameplaySystem.players[turn].GetComponent<Player>().property.Add(spaceIndex);
 
            if(allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().isUtility)
                GameplaySystem.players[turn].GetComponent<Player>().numUtilities += 1;

           float costToPurchaseProperty = 0;
            if(allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().isUtility)
            {
                costToPurchaseProperty = (float) (allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().propertyCard as UtilitiesCard).value; 
            }

            else if(allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().isRailroad)
            {
                GameplaySystem.players[turn].GetComponent<Player>().numRailroads += 1;
                costToPurchaseProperty = (float) (allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().propertyCard as RailroadCard).value;
            }

            else
            {
                string color = (allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().propertyCard as PropertyCard).colorCategory.Trim();
                GameplaySystem.players[turn].GetComponent<Player>().fullSetTracker[color]++;  
                costToPurchaseProperty = (float) (allBoardSpaces[spaceIndex].GetComponent<PropertySpace>().propertyCard as PropertyCard).purchaseCost;
            }
            GameplaySystem.players[turn].GetComponent<Player>().money -= costToPurchaseProperty;
        }

        // Last thing to occur in order to continue gameplay (turns)
        cont_changeTurns = true; 
    }
}