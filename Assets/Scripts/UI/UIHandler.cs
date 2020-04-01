﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIHandler : SingletonMonoBehaviour<UIHandler>
{
    public static event Action StartGame;
    public static event Action RollDice;
    public static event Action PauseGame;
    public static event Action ResumeGame;

    public GameObject Splash;
    public Button StartButton;
    public Button ResumeButton;
    public Button RollDiceButton;
    public Button RollDiceButtonDisabled;
    public Button HomePauseButton;


    void Start()
    {
        try {

            StartButton.onClick.AddListener(StartGameButtonClick);
            ResumeButton.onClick.AddListener(ResumeGameButtonClick);
            RollDiceButton.onClick.AddListener(RollDiceClick);
            HomePauseButton.onClick.AddListener(PauseGameButtonClick);
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
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.Menu);
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

}
