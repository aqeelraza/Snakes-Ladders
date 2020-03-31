using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GamePlayManager : MonoBehaviour
{

    public Transform FirstBox;
    public Transform SecondBox;



    void Start()
    {
        try{
            GameConstants.FirstBoxPosition = FirstBox.position;
            GameConstants.OneBoxDistance = SecondBox.position.x - FirstBox.position.x;
        }
        catch (NullReferenceException e) {
            Debug.unityLogger.Log(GameConstants.MissingReferenceTag, e);
        }

    }


}
