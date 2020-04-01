using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
{

    public enum GameState { 
        SplashLoading, 
        StartMenu,
        GameStarted,
        RollDiced,
        PlayerMoving,
        WaitingForDiceToRoll,
        GamePause,
        GameEnd
    }

    GameState gameState;
    GameState previousGameState; 

    void Start()
    {   
        previousGameState = GameState.SplashLoading;
        gameState = GameState.SplashLoading;
        UIHandler.Instance.WaitAndRemoveSplash();
    }

    public void SetGameState(GameState _gameState) {
        Debug.Log(_gameState);
        previousGameState = gameState;
        this.gameState = _gameState;
    }

    public GameState GetGameState() {
        return gameState;
    }

    public void SetPreviousState() {
        GameState tempState = gameState;
        gameState = previousGameState;
        previousGameState = tempState;
    }

}
