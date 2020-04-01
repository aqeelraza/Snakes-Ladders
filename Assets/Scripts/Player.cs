using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// This script on the players and perform the player functionality.
/// </summary>
public class Player : MonoBehaviour
{
    int currentStep = 0;
    bool isPlayerMoving = false;

    public static event Action ChangeTurn;
    Vector3 originalPosition;
    private void Start()
    {
        originalPosition = transform.position;
    }
    public void SpawnPlayer() {
        iTween.FadeFrom(this.gameObject, iTween.Hash("alpha", 0, "time", 1.0f, "easeType", "easeInSine", "oncomplete", "FadeToFullAlpha"));


    }
    void FadeToFullAlpha() {
        iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 1));
    }


    public int GetUpdatedStep(int steps) {
        if (currentStep + steps > 100) {
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
    /// <summary>
    /// This function moves the player on board. 
    /// </summary>
    /// <param name="step">Step.</param>
    /// <param name="isSnakeOrLadder">If set to <c>true</c> is snake or ladder.</param>
    public void MovePlayer(int step,bool isSnakeOrLadder) {

        int rows = (currentStep - 1) / 10;
        int cols = (currentStep - 1) % 10;
        if (rows % 2 != 0)//this logic is because half the rows are from left to right.
        {
            cols = 9 - cols;
        }

        Vector3 newPos = GameConstants.FirstBoxPosition + new Vector3(GameConstants.OneBoxDistance * cols, GameConstants.OneBoxDistance * rows, 0);
        isPlayerMoving = true;
        if (!isSnakeOrLadder) {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", newPos, "time", 1f, "oncomplete", "OnMovementComplete"));
        }
        else {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", newPos, "time", step * GameConstants.PlayerMovementSpeed));
        }
    }

    void OnMovementComplete() {
        isPlayerMoving = false;
        ChangeTurn();
    }
    public void ResetPlayer()
    {
        transform.position = originalPosition;
        currentStep = 0;
        isPlayerMoving = false;
    }
}
