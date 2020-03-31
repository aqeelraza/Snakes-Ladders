using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private static UIHandler myInstance = null;
    public GameObject Splash;
    public Button StartButton;
    public Button ResumeButton;

    void Awake()
    {
        myInstance = this;
    }

    public static UIHandler getInstance()
    {
        if (myInstance == null)
        {
            myInstance = new UIHandler();
        }
        return myInstance;
    }

    void Start()
    {
        try {

            StartButton.onClick.AddListener(StartGameButtonClick);
            //ResumeButton.onClick.AddListener(ResumeGameButtonClick);

            Splash.gameObject.SetActive(true);
        } catch(NullReferenceException e) {
            Debug.unityLogger.Log(UIConstants.MissingReferenceTag, e);
        }
    }

    public void StartGameButtonClick()
    {
        if (!UIConstants.Instance.GameStarted) {
            UIConstants.Instance.GameStarted = true;
            StartButton.gameObject.SetActive(false);
        }
        GameAnimationManager.getInstance().AnimateBoardToGameCenter();
    }


    public void BackToStartMenu() {
        if (UIConstants.Instance.GameStarted)
        {
            UIConstants.Instance.GameStarted = false;
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
        GameStateManager.getInstance().SetGameState(GameStateManager.GameState.Menu);
        iTween.ShakePosition(StartButton.gameObject,iTween.Hash("x",20,"time",1.2f));

    }

}
