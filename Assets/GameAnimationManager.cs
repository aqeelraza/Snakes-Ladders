using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnimationManager : MonoBehaviour
{

    public GameObject GameBoard;
    public Transform GameCenter;

    private static GameAnimationManager myInstance = null;

    void Awake()
    {
        myInstance = this;
    }

    public static GameAnimationManager getInstance()
    {
        if (myInstance == null)
        {
            myInstance = new GameAnimationManager();
        }
        return myInstance;
    }


    public void AnimateBoardToGameCenter() {
        GameStateManager.getInstance().SetGameState(GameStateManager.GameState.GameInProgress);
        iTween.MoveTo(GameBoard, iTween.Hash("position",GameCenter.position,"time",1f));
    }
}
