using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private int turn = 1;
    private bool coroutine = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];
    }

    private void OnMouseDown() 
    {
        if(coroutine)
            StartCoroutine("RollTheDice");
    }

    // Update is called once per frame
    private IEnumerator RollTheDice()
    {
        coroutine = false;
        int randomDiceSide = 0;
        for(int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0,6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        /*
        GameplaySystem.diceSideThrown = randomDiceSide + 1;
        if(turn >= 5)
            turn = 1;
        GameplaySystem.MovePlayer(turn);
        turn += 1;
        */
        coroutine = true;
    }
}
