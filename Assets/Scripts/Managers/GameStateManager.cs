using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
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


    public static Action WaitAndRemoveSplash;
    public GameState gameState { get; private set; }
    GameState previousGameState; 

    void Start()
    {
        GamePlayManager.UpdateGameState += SetGameState;
        UIHandler.UpdateGameState += SetGameState;
        UIHandler.SetPreviousState += SetPreviousState;
        previousGameState = GameState.SplashLoading;
        gameState = GameState.SplashLoading;
        if (WaitAndRemoveSplash!=null) {
            WaitAndRemoveSplash();
        }
    }

    public void SetGameState(GameState _gameState) {
        previousGameState = gameState;
        this.gameState = _gameState;
    }


    public void SetPreviousState() {
        GameState tempState = gameState;
        gameState = previousGameState;
        previousGameState = tempState;
    }

}
