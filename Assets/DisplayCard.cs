using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{
	public static bool coroutine = false;
	public static int cardIdx; 
	private Sprite[] cardsToDisplay;
	private SpriteRenderer rend;
	public static bool stopDisplay = false;
    // Update is called once per frame
	
	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		cardsToDisplay = Resources.LoadAll<Sprite>("BoardCards");
	}

    void Update()
    {
        if(coroutine)
		{
			StartCoroutine("DisplayACard");
		}
    }
	
	private IEnumerator DisplayACard()
	{
			stopDisplay = false;
			coroutine = false; 
			//rend.sprite = cardToDisplay;
			string cardToDisplay = "BoardCards/card" + cardIdx;
			Debug.Log(cardToDisplay);
			rend.sprite = Resources.Load<Sprite>(cardToDisplay);

			while(!stopDisplay)
			{
				yield return new WaitForSeconds(.5f);
			}
			rend.sprite = null;
	}
}
