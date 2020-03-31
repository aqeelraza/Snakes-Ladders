using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnimationManager : SingletonMonoBehaviour<GameAnimationManager>
{

    public void AnimateBoardToGameCenter() {
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameInProgress);
        GameBoard.Instance.Spawn();
    }

    public void AnimatePlayersSpawning() {
        PlayerManager.Instance.SpawnPlayers();
    }
}
