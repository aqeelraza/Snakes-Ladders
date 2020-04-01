using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Main class to handle all the UI events. 
/// </summary>
public class UIHandler : SingletonMonoBehaviour<UIHandler>
{
    public static event Action StartGame;
    public static event Action RollDice;
    public static event Action PauseGame;
    public static event Action ResumeGame;
    public static event Action RestartGame;

    public GameObject Splash;
    public Button StartButton;
    public Button ResumeButton;
    public Button RollDiceButton;
    public Button RollDiceButtonDisabled;
    public Button HomePauseButton;
    public GameObject EndGamePanel;
    public Text EndGameText;
    public Button QuitGameButton;
    public Text PlayerTurnNote;
    public Button RestartGameButton;

    void Start()
    {
        try {
            StartButton.onClick.AddListener(StartGameButtonClick);
            ResumeButton.onClick.AddListener(ResumeGameButtonClick);
            RollDiceButton.onClick.AddListener(RollDiceClick);
            HomePauseButton.onClick.AddListener(PauseGameButtonClick);
            QuitGameButton.onClick.AddListener(EndGame);
            RestartGameButton.onClick.AddListener(RestartGameClick);
            Splash.gameObject.SetActive(true);
        } catch(NullReferenceException e) {
            Debug.unityLogger.Log(UIConstants.MissingReferenceTag, e);
        }
    }

    public void StartGameButtonClick()
    {
        if (!UIConstants.GameStarted) {
            UIConstants.GameStarted = true;
            StartButton.gameObject.SetActive(false);
        }
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameStarted);
        Invoke("ShowGameButtons",1.0f);
        StartGame();
    }
    void ShowGameButtons() {
        RollDiceButton.gameObject.SetActive(true);
        HomePauseButton.gameObject.SetActive(true);
        PlayerTurnNote.gameObject.SetActive(true);
    }

    public void BackToStartMenu() {
        if (UIConstants.GameStarted)
        {
            UIConstants.GameStarted = false;
            StartButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGameButtonClick() {
        GameStateManager.Instance.SetPreviousState();
        ResumeGame();
        ResumeButton.gameObject.SetActive(false);
        RollDiceButton.gameObject.SetActive(true);
        HomePauseButton.gameObject.SetActive(true);
    }


    public void PauseGameButtonClick() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.GamePause);
        PauseGame();
        RollDiceButton.gameObject.SetActive(false);
        HomePauseButton.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(true);
    }

    public void WaitAndRemoveSplash() {
        iTween.FadeTo(Splash, iTween.Hash("alpha", 0f, "time", 0.5f, "delay", 2f, "onComplete", "ShowMenu", "oncompletetarget",this.gameObject));
    }

    void ShowMenu() { 
        if(Splash != null) {
            Destroy(Splash);
        }
        StartButton.gameObject.SetActive(true);
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.StartMenu);
        iTween.ShakePosition(StartButton.gameObject,iTween.Hash("x",30,"time",0.4f));

    }

    public void RollDiceClick() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.RollDiced);
        RollDiceButton.GetComponent<Button>().enabled = false;
        RollDiceButtonDisabled.gameObject.SetActive(true);
        RollDiceButton.gameObject.SetActive(false);
        RollDice();
    }

    public void ChangeRollDiceStatus() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.WaitingForDiceToRoll);
        RollDiceButtonDisabled.gameObject.SetActive(false);
        RollDiceButton.gameObject.SetActive(true);
        RollDiceButton.GetComponent<Button>().enabled = true;
    }

    public void ShowEndGamePanel()
    {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameEnd);
        EndGameText.text = GameConstants.WinningPlayer + "has won the game.";
        EndGamePanel.SetActive(true);
    }

    public void ShowTurnText(string text) {
        if (text.Contains("1")) {
            PlayerTurnNote.color = Color.blue;
        }
        else {
            PlayerTurnNote.color = Color.red;
        }
        PlayerTurnNote.text = text;
    }
    public void ResetUI()
    {
        PlayerTurnNote.text = "";
        RollDiceButton.gameObject.SetActive(true);
        RollDiceButton.GetComponent<Button>().enabled = true;
        RollDiceButtonDisabled.gameObject.SetActive(false);
        EndGamePanel.gameObject.SetActive(false);
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.StartMenu);
    }
    public void RestartGameClick() {
        RestartGame();
    }
    public void EndGame() {
        Application.Quit();
    }

}
