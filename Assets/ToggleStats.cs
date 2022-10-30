using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ToggleStats : MonoBehaviour
{
    public static GameObject StatsPanel;
    public static GameObject OwnedPanel;
    [SerializeField] public TextMeshPro displayStatus;
    public Button t_Btn;
    static int index = 2;

    // Start is called before the first frame update
    void Start()
    {
        OwnedPanel = GameObject.Find("OwnedPanel");
        StatsPanel = GameObject.Find("StatsPanel");

        Button tBtn = t_Btn.GetComponent<Button>();
        tBtn.onClick.AddListener(Toggle);
    }

    private void Toggle()
	{
        OwnedPanel.SetActive(false);
        StatsPanel.SetActive(false);

        switch(index){
            case 0:
                OwnedPanel.SetActive(true);
                displayStatus.text = "Current Player";
                break;
            case 1:
                StatsPanel.SetActive(true);
                displayStatus.text = "All Players";
                break;
            default:
                displayStatus.text = "";
                break;
        }
        index = (index+1) % 3;
    }
}
