using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{
	public static bool coroutine = false;
	private Sprite cardToDisplay;
	private SpriteRenderer rend;
	public static bool stopDisplay = false;
    // Update is called once per frame
	
	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		cardToDisplay = Resources.Load<Sprite>("Properties/testCard");
	}
    void Update()
    {
        if(coroutine)
			StartCoroutine("DisplayACard");
    }
	
	private IEnumerator DisplayACard()
	{
			stopDisplay = false;
			coroutine = false; 
			rend.sprite = cardToDisplay;
			while(!stopDisplay)
			{
				yield return new WaitForSeconds(.5f);
			}
			
			rend.sprite = null;
	}
}
