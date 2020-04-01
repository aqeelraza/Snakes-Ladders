using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This is the main class which handles the game logic.
/// Gets the input from UI events
/// Run Animations
/// Manages Players and turns
/// </summary>
public class GamePlayManager : MonoBehaviour
{


    public Transform FirstBox;
    public Transform SecondBox;
    bool gameEnd = false;

    class Connection {
        public int startStep;
        public int endStep;
        public Connection(int _startStep ,int _endStep) {
            startStep = _startStep;
            endStep = _endStep;
        }
    }
    //These are hard code values for snake and ladders connections , they are hard coded after setting up the board.
    Connection[] Snakes = { new Connection(15,4), new Connection(36,26) , new Connection(69,48), new Connection(94,67), new Connection(97,39) };
    Connection[] Ladders = { new Connection(3, 23), new Connection(14, 33), new Connection(41,58), new Connection(47, 98), new Connection(71, 90) };

    void Start()
    {
        PlayerManager.CheckForSnakeOrLadder += CheckForSnakeOrLadder;
        PlayerManager.GameEnd += GameEnd;
        Player.ChangeTurn += ChangeTurn;
        UIHandler.StartGame += StartGame;
        UIHandler.RollDice += RollDice;
        UIHandler.PauseGame += PauseGame;
        UIHandler.ResumeGame += ResumeGame;
        UIHandler.RestartGame += RestartGame;
        try
        {
            GameConstants.FirstBoxPosition = FirstBox.position;
            GameConstants.OneBoxDistance = SecondBox.position.x - FirstBox.position.x;
        }
        catch (NullReferenceException e) {
            Debug.unityLogger.Log(GameConstants.MissingReferenceTag, e);
        }

    }

    //This function basically checks if the player had landed on the snake or ladder
    int CheckForSnakeOrLadder(int currentStep)
    {
        for (int i = 0;i<Snakes.Length;i++) { //because snakes and ladders are equal
            if (currentStep == Snakes[i].startStep) {

                return Snakes[i].endStep;
            }else if (currentStep == Ladders[i].startStep)
            {
                return Ladders[i].endStep;
            }
        }
        return 0;
    }

    void StartGame() {
        GameAnimationManager.Instance.AnimateBoardToGameCenter();
        UIHandler.Instance.ShowTurnText("Player1 turn ");
    }


    void ChangeTurn() {
        if (gameEnd)
        {
            UIHandler.Instance.ShowEndGamePanel();
        }
        else {
            PlayerManager.Instance.ChangePlayer();
            UIHandler.Instance.ShowTurnText(PlayerManager.Instance.activePlayer.name  +" turn");
            UIHandler.Instance.ChangeRollDiceStatus();
            GameAnimationManager.Instance.RemoveDice();
        }
    }

    void RollDice() {
        int num = UnityEngine.Random.Range(1,7);
        GameAnimationManager.Instance.AnimateDice(num);
        StartCoroutine(MovePlayer(num));
    }

    IEnumerator MovePlayer(int step) {
        yield return new WaitForSeconds(1.5f);
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.PlayerMoving);
        PlayerManager.Instance.MovePlayer(step);
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }
    public void ResumeGame() {
        Time.timeScale = 1;
    }
    public void GameEnd()
    {
        gameEnd = true;
    }

    public void RestartGame() {
        gameEnd = false;
        PlayerManager.Instance.ResetPlayers();
        DiceManager.Instance.RemoveDice();
        UIHandler.Instance.ResetUI();
    }
}
