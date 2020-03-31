using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int currentStep = 0;

    public void SpawnPlayer() {
        iTween.FadeFrom(this.gameObject, iTween.Hash("alpha", 0, "time", 1.0f, "easeType", "easeInSine", "oncomplete", "FadeToFullAlpha"));


    }
    void FadeToFullAlpha() {
        iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 1));
    }


    public int MoveToNewStep(int steps) {
        if (currentStep + steps > 100) {

            //ToDo 
            //play sound
            return -1;
        }else if (currentStep + steps == 100) { 
            //ToDo
            //Player has won
        }
        currentStep += steps;
        return MovePlayer(steps);
    }

    public void MoveToNewPositin(int newPosition) {
        int steps = Mathf.Abs(currentStep - newPosition);
        currentStep = newPosition;
        MovePlayer(steps);
    }

    int MovePlayer(int steps) {

        int rows = (currentStep - 1) / 10;
        int cols = (currentStep - 1) % 10;
        if (rows % 2 != 0)
        {
            cols = 9 - cols;
        }

        Vector3 newPos = GameConstants.FirstBoxPosition + new Vector3(GameConstants.OneBoxDistance * cols, GameConstants.OneBoxDistance * rows, 0);
        iTween.MoveTo(this.gameObject, iTween.Hash("position", newPos, "time", steps * 0.1f));
        return currentStep;
    }
}
