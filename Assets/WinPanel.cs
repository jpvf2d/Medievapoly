using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public static GameObject[] players;
    public Button m_Btn;
    public Button q_Btn;

    void Start(){
        players = new GameObject[] {GameObject.Find("Player1"), GameObject.Find("Player2"), GameObject.Find("Player3"), GameObject.Find("Player4")};
        Button mBtn = m_Btn.GetComponent<Button>();
        mBtn.onClick.AddListener(MainMenu);
        Button qBtn = q_Btn.GetComponent<Button>();
        qBtn.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        ChangeSprite();
    }

    void ChangeSprite()
    {
        for(int i = 0; i < players.Length; i++){
            if(players[i].GetComponent<Player>().isWinner){
                switch(i)
                {
                    case 0:
                        spriteRenderer.sprite = sprite1; 
                        break;
                    case 1:
                        spriteRenderer.sprite = sprite2;
                        break;
                    case 2:
                        spriteRenderer.sprite = sprite3;
                        break;
                    case 3:
                        spriteRenderer.sprite = sprite4;
                        break;
                } 
            }
        }
    }

    public void MainMenu(){
        Debug.Log("Navigating to Main Menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame(){
        Debug.Log("Quitting Game (Only Works Outside Unity)");
        Application.Quit();
    }
}
