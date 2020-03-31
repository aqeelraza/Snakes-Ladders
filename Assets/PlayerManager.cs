using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public delegate int TempDelegtate(int steps);
    public static event Func<int, int> CheckForSnakeOrLadder;

    public GameObject PlayerHuman;
    public GameObject PlayerAI;

    private void Start()
    {

    }

    public void SpawnPlayers() {
        PlayerHuman.SetActive(true);
        PlayerAI.SetActive(true);

        PlayerHuman.GetComponent<Player>().SpawnPlayer();
        PlayerAI.GetComponent<Player>().SpawnPlayer();
    }



    private void Update()
    {
        if (Input.anyKeyDown) {
            MovePlayer();
        }
    }

    public void MovePlayer() {
        int step = UnityEngine.Random.Range(1, 6);
        int currentStep = PlayerHuman.GetComponent<Player>().MoveToNewStep(step);
        int SnakeOrLadderPos = CheckForSnakeOrLadder(currentStep);
        if(SnakeOrLadderPos != 0) {
            Debug.Log(SnakeOrLadderPos);
            PlayerHuman.GetComponent<Player>().MoveToNewPositin(SnakeOrLadderPos);
        }
    }

}
