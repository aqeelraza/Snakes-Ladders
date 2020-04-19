using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Main class to handle all the UI events. 
/// </summary>
public class UIHandler : MonoBehaviour
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

    public static Action<GameStateManager.GameState> UpdateGameState;
    public static Action SetPreviousState;
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
            GameStateManager.WaitAndRemoveSplash += WaitAndRemoveSplash;
            GamePlayManager.ShowTurnText += ShowTurnText;
            GamePlayManager.ChangeRollDiceStatus += ChangeRollDiceStatus;
            GamePlayManager.ShowEndGamePanel += ShowEndGamePanel;
            GamePlayManager.ResetUI += ResetUI;
            }
        catch(NullReferenceException e) {
            Debug.unityLogger.Log(UIConstants.MissingReferenceTag, e);
        }
    }

    public void StartGameButtonClick()
    {
        if (!UIConstants.GameStarted) {
            UIConstants.GameStarted = true;
            StartButton.gameObject.SetActive(false);
        }
        UpdateGameState(GameStateManager.GameState.GameStarted);
        Invoke("ShowGameButtons",1.0f);
        if (StartGame != null) {
            StartGame();
        }

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
        SetPreviousState();
        if (ResumeGame!=null) {
            ResumeGame();
        }
        ResumeButton.gameObject.SetActive(false);
        RollDiceButton.gameObject.SetActive(true);
        HomePauseButton.gameObject.SetActive(true);
    }


    public void PauseGameButtonClick() {
        UpdateGameState(GameStateManager.GameState.GamePause);
        if (PauseGame != null) {
            PauseGame();
        }
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
        UpdateGameState(GameStateManager.GameState.StartMenu);
        iTween.ShakePosition(StartButton.gameObject,iTween.Hash("x",30,"time",0.4f));

    }

    public void RollDiceClick() {
        UpdateGameState(GameStateManager.GameState.RollDiced);
        RollDiceButton.GetComponent<Button>().enabled = false;
        RollDiceButtonDisabled.gameObject.SetActive(true);
        RollDiceButton.gameObject.SetActive(false);
        if(RollDice != null) {
            RollDice();
        }
    }

    public void ChangeRollDiceStatus() {
        UpdateGameState(GameStateManager.GameState.WaitingForDiceToRoll);
        RollDiceButtonDisabled.gameObject.SetActive(false);
        RollDiceButton.gameObject.SetActive(true);
        RollDiceButton.GetComponent<Button>().enabled = true;
    }

    public void ShowEndGamePanel()
    {
        UpdateGameState(GameStateManager.GameState.GameEnd);
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
        UpdateGameState(GameStateManager.GameState.StartMenu);
    }
    public void RestartGameClick() {
        if (RestartGame != null) {
            RestartGame();
        }

    }
    public void EndGame() {
        Application.Quit();
    }

}
