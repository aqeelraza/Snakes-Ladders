using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{

    public enum GameState { 
        SplashLoading , 
        Menu,
        GameInProgress,
        GamePause
    }

    private static GameStateManager myInstance = null;
    GameState gameState;

    void Awake()
    {
        myInstance = this;
    }

    public static GameStateManager getInstance() {
        if(myInstance == null) {
            myInstance = new GameStateManager();
        }
        return myInstance;
    }
    
    void Start()
    {
        gameState = GameState.SplashLoading;
        UIHandler.getInstance().WaitAndRemoveSplash();

    }



    public void SetGameState(GameState _gameState) {
        this.gameState = _gameState;
    }

    public GameState GetGameState() {
        return gameState;
    }

}
