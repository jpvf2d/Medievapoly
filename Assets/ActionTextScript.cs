using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionTextScript : MonoBehaviour
{
    public static GameObject ActionText;
    public static bool displayActionCanvas = false;

    void Start()
    {
        ActionText = GameObject.Find("ActionText");       
        ActionText.SetActive(false); 
    }

    public static IEnumerator display(string textToDisplay)
    {
        ActionText.GetComponent<TMP_Text>().text = textToDisplay;
        ActionText.SetActive(true);

        // https://owlcation.com/stem/How-to-fade-out-a-GameObject-in-Unity#:~:text=Fade%20Objects%20in%20Unity%201%20Change%20the%20GameObject%27s,Update%20Boolean.%203%20Use%20a%20Coroutine.%20See%20More.
        // Fade out text 
        Color objectColor = ActionText.GetComponent<TMP_Text>().color;
       
        while(objectColor.a >= 0 )       
        {
            float fadeAmt = objectColor.a - (2 * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmt);
            ActionText.GetComponent<TMP_Text>().color = objectColor;
            yield return new WaitForSeconds(0.01f);
        }

        ActionText.SetActive(false);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        ActionText.GetComponent<TMP_Text>().color = objectColor; 
    }

}