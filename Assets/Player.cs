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


    public void MovePlayer(int steps) {
        if (currentStep + steps > 100) {

            //ToDo 
            //play sound
            return;
        }
        currentStep += steps;
        int rows = currentStep % 10;
        int cols = currentStep / 10;

        Vector3 newPos = GameConstants.FirstBoxPosition + new Vector3(GameConstants.OneBoxDistance * rows, GameConstants.OneBoxDistance * cols, 0);
        iTween.MoveTo(this.gameObject,iTween.Hash("position",newPos,"time",steps*0.1f));
    }

}
