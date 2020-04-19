using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is where players are being managed.
/// Right now only two players are here but we can add more players and update the logic.
/// </summary>
public class PlayerManager : MonoBehaviour
{

    public static event Func<int, int> CheckForSnakeOrLadder;
    public static event Action GameEnd;
    public static Player activePlayer { get; private set; }

    public GameObject PlayerHuman;
    public GameObject PlayerAI;



    private void Start()
    {
        GameAnimationManager.SpawnPlayers += SpawnPlayers;
        GamePlayManager.ChangePlayer += ChangePlayer;
        GamePlayManager.MovePlayerAction += MovePlayer;
        GamePlayManager.ResetPlayers += ResetPlayers;
        activePlayer = PlayerHuman.GetComponent<Player>();
    }

    public void SpawnPlayers() {
        PlayerHuman.SetActive(true);
        PlayerAI.SetActive(true);
        PlayerHuman.GetComponent<Player>().SpawnPlayer();
        PlayerAI.GetComponent<Player>().SpawnPlayer();
        activePlayer = PlayerHuman.GetComponent<Player>();
    }



    public void MovePlayer(int step) {
        int playerNewStep = activePlayer.GetUpdatedStep(step);
        if (playerNewStep ==100) {
            GameConstants.WinningPlayer = activePlayer.name;
            GameEnd();
        }

        int SnakeOrLadderPos = CheckForSnakeOrLadder(playerNewStep);
        if(SnakeOrLadderPos != 0) {
            activePlayer.MovePlayer(SnakeOrLadderPos, true);
            StartCoroutine(MoveAfterSnakeOrLadder(step*GameConstants.PlayerMovementSpeed+0.3f, SnakeOrLadderPos));
        }
        else
        {
            activePlayer.MovePlayer(SnakeOrLadderPos,false);
        }
    }

    IEnumerator MoveAfterSnakeOrLadder(float seconds,int SnakeOrLadderPos)
    {
        yield return new WaitForSeconds(seconds);
        activePlayer.MoveToNewPositin(SnakeOrLadderPos);
    }



    public void ChangePlayer() { 

        if(activePlayer.name.Equals("Player1"))
        {
            activePlayer = PlayerAI.GetComponent<Player>();
        }
        else {
            activePlayer = PlayerHuman.GetComponent<Player>();
        }
    }

    public void ResetPlayers() {
        PlayerHuman.GetComponent<Player>().ResetPlayer();
        PlayerAI.GetComponent<Player>().ResetPlayer();
    }

}
