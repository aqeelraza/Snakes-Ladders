using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    int currentStep = 0;
    bool isPlayerMoving = false;

    public static event Action ChangeTurn;

    public void SpawnPlayer() {
        iTween.FadeFrom(this.gameObject, iTween.Hash("alpha", 0, "time", 1.0f, "easeType", "easeInSine", "oncomplete", "FadeToFullAlpha"));


    }
    void FadeToFullAlpha() {
        iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 1));
    }


    public int GetUpdatedStep(int steps) {
        if (currentStep + steps > 100) {

            //ToDo 
            //play sound
            return -1;
        }else if (currentStep + steps == 100) {
            return 100;
        }
        currentStep += steps;
        return currentStep;
    }

    public void MoveToNewPositin(int newPosition) {
        int steps = Mathf.Abs(currentStep - newPosition);
        currentStep = newPosition;
        MovePlayer(steps,false);
    }

    public void MovePlayer(int step,bool isSnakeOrLadder) {

        int rows = (currentStep - 1) / 10;
        int cols = (currentStep - 1) % 10;
        if (rows % 2 != 0)
        {
            cols = 9 - cols;
        }

        Vector3 newPos = GameConstants.FirstBoxPosition + new Vector3(GameConstants.OneBoxDistance * cols, GameConstants.OneBoxDistance * rows, 0);
        isPlayerMoving = true;
        if (!isSnakeOrLadder) {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", newPos, "time", step * GameConstants.PlayerMovementSpeed, "oncomplete", "OnMovementComplete"));
        }
        else {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", newPos, "time", step * GameConstants.PlayerMovementSpeed));
        }
    }

    void OnMovementComplete() {
        isPlayerMoving = false;
        ChangeTurn();
    }
}
