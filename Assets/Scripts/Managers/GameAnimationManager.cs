using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A very simple class to handle the in game animations.
/// </summary>
public class GameAnimationManager : SingletonMonoBehaviour<GameAnimationManager>
{

    public void AnimateBoardToGameCenter() {
        GameBoard.Instance.Spawn();
    }

    public void AnimatePlayersSpawning() {
        PlayerManager.Instance.SpawnPlayers();
    }

    public void AnimateDice(int num) {
        DiceManager.Instance.RollDice(num);
    }

    public void RemoveDice() {
        DiceManager.Instance.RemoveDice();
    }
}
