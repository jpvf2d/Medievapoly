using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public bool db_coroutine = false;
    public int DiceNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];
    }

    public IEnumerator RollTheDice(int val)
    {
        int randomDiceSide = 0;
        for(int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0,6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }
        rend.sprite = diceSides[val];
        DiceNum = val;
        db_coroutine = true;
        //GameplaySystem.MovePlayer();
    }

    private void OnMouseDown() 
    {
        GameplaySystem.roll();
    }
}
