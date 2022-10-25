using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeParking : BoardSpace
{
    public int index; 

    public override void passing()
    {
        Debug.Log("Do nothing, land:FreeParking");
    }

    public override void land()
    {
        StartCoroutine(ActionTextScript.display("Free parking"));
        SpaceLogic.cont_changeTurns = true;
    }

    

    public override void stuck()
    {
        Debug.Log("Do nothing, stuck:FreeParking");
    }

    
}
