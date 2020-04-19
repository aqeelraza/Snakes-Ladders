using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// A very simple class to handle the in game animations.
/// </summary>
public class GameAnimationManager : MonoBehaviour
{

    public static Action SpawnPlayers;
    public static Action<int> RollDice;
    public static Action RemoveDiceAction;
    public static Action SpawnBoard;

    void Start()
    {
        GamePlayManager.AnimateDice += AnimateDice;
        GamePlayManager.AnimateBoardToGameCenter += AnimateBoardToGameCenter;
        GamePlayManager.RemoveDice += RemoveDice;

    }
    public void AnimateBoardToGameCenter() {
        SpawnBoard();
    }

    public void AnimatePlayersSpawning() {
        if (SpawnPlayers != null) {
            SpawnPlayers();
        }
    }

    public void AnimateDice(int num) {

        if (RollDice != null) {
            RollDice(num);
        }

    }

    public void RemoveDice() {
        if (RemoveDiceAction!=null) {
            RemoveDiceAction();
        }
    }
}
