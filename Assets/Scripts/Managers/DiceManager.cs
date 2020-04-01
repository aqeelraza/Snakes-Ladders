using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Simple manager to show Dice and run its animations.
/// </summary>
public class DiceManager : SingletonMonoBehaviour<DiceManager>
{
    GameObject activeDice;
    public void RollDice(int num) {
        activeDice = GameObject.Find("Dice" + num);
        try {
            activeDice.GetComponent<SpriteRenderer>().enabled = true;
            activeDice.GetComponent<Animator>().enabled = true;
            activeDice.GetComponent<Animator>().Play("Dice" + num, -1, 0f);
        }
        catch(NullReferenceException e)
        {
            Debug.unityLogger.Log(GameConstants.MissingGOTag, e);
        }
    }

    public void RemoveDice()
    {
        activeDice.GetComponent<SpriteRenderer>().enabled = false;
        activeDice.GetComponent<Animator>().enabled = false;
    }

}
