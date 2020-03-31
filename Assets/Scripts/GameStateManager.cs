using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
{

    public enum GameState { 
        SplashLoading , 
        Menu,
        GameInProgress,
        GamePause
    }


    GameState gameState;


    
    void Start()
    {
        gameState = GameState.SplashLoading;

        UIHandler.Instance.WaitAndRemoveSplash();

    }



    public void SetGameState(GameState _gameState) {
        this.gameState = _gameState;
    }

    public GameState GetGameState() {
        return gameState;
    }

}
