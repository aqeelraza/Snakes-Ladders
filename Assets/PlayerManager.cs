using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{

    public static event Func<int, int> CheckForSnakeOrLadder;



    public GameObject PlayerHuman;
    public GameObject PlayerAI;

    Player activePlayer;

    private void Start()
    {
        activePlayer = PlayerHuman.GetComponent<Player>();
    }
    public void SpawnPlayers() {
        PlayerHuman.SetActive(true);
        PlayerAI.SetActive(true);

        PlayerHuman.GetComponent<Player>().SpawnPlayer();
        PlayerAI.GetComponent<Player>().SpawnPlayer();
        activePlayer = PlayerHuman.GetComponent<Player>();
    }



    public void MovePlayer() {

        int step = UnityEngine.Random.Range(1, 6);
        int playerNewStep = activePlayer.GetUpdatedStep(step);
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

        if(activePlayer.name.Equals("PlayerHuman"))
        {
            activePlayer = PlayerAI.GetComponent<Player>();
        }
        else {
            activePlayer = PlayerHuman.GetComponent<Player>();
        }
    }

}
