using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIHandler : SingletonMonoBehaviour<UIHandler>
{
    public static event Action StartGame;
    public static event Action RollDice;


    public GameObject Splash;
    public Button StartButton;
    public Button ResumeButton;
    public Button RollDiceButton;

   
    void Start()
    {
        try {

            StartButton.onClick.AddListener(StartGameButtonClick);
            //ResumeButton.onClick.AddListener(ResumeGameButtonClick);
            RollDiceButton.onClick.AddListener(RollDiceClick);
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
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameInProgress);
        Invoke("ShowDiceButton",1.0f);

        StartGame();
    }
    void ShowDiceButton() {
        RollDiceButton.gameObject.SetActive(true);
    }

    public void BackToStartMenu() {
        if (UIConstants.GameStarted)
        {
            UIConstants.GameStarted = false;
            StartButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGameButtonClick() { 

    }


    public void PauseGameButtonClick() { 
    
    }

    public void WaitAndRemoveSplash() {
        iTween.FadeTo(Splash, iTween.Hash("alpha", 0f, "time", 0.5f, "delay", 2f, "onComplete", "ShowMenu", "oncompletetarget",this.gameObject));
    }

    void ShowMenu() { 
        if(Splash != null) {
            Destroy(Splash);
        }
        StartButton.gameObject.SetActive(true);
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.Menu);
        iTween.ShakePosition(StartButton.gameObject,iTween.Hash("x",30,"time",0.4f));

    }

    public void RollDiceClick() {
        RollDiceButton.GetComponent<Button>().enabled = false;
        RollDice();
    }

    public void ChangeRollDiceStatus() {
        RollDiceButton.GetComponent<Button>().enabled = true;
    }

}
