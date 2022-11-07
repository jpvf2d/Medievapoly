using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    float slowSpeed = 18f;
    float mediumSpeed = 30f;
    float fastSpeed = 40f;
    Player currentPlayer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ChangeSpeed(){
        int turn = GameplaySystem.turn;
        currentPlayer = GameplaySystem.players[turn].GetComponent<Player>();
        if(currentPlayer.moveSpeed == slowSpeed){
            GameplaySystem.players[turn].GetComponent<Player>().moveSpeed = mediumSpeed;
            StartCoroutine(ActionTextScript.display("Player's Speed:\nQuick"));
        }
        else if(currentPlayer.moveSpeed == mediumSpeed){
            currentPlayer.moveSpeed = fastSpeed;
            StartCoroutine(ActionTextScript.display("Player's Speed:\nUltra Fast"));
        }
        else{
            currentPlayer.moveSpeed = slowSpeed;
            StartCoroutine(ActionTextScript.display("Player's Speed:\nNormal"));
        }
    
    }

    public void ToggleMusic(){
        if(AudioListener.volume == 1){
            AudioListener.volume = 0;
            StartCoroutine(ActionTextScript.display("Music Disabled"));
        }
        else{
            AudioListener.volume = 1;
            StartCoroutine(ActionTextScript.display("Music Enabled"));
        }        
    }

    public void QuitGame(){
        Debug.Log("Quitting Game (Only Works Outside Unity)");
        Application.Quit();
    }

    public void AutoPlay() {
        int turn = GameplaySystem.turn;
        currentPlayer = GameplaySystem.players[turn].GetComponent<Player>();

        if (currentPlayer.autoPlayEnabled) {
            currentPlayer.autoPlayEnabled = false;
            StartCoroutine(ActionTextScript.display("Autoplay disabled"));
        }
        else {
            currentPlayer.autoPlayEnabled = true;
            StartCoroutine(ActionTextScript.display("Autoplay enabled"));
        }
    }
}
