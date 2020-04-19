using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public Transform GameCenter;
    public GameObject gameAnimatorObject;

    private void Start()
    {
        GameAnimationManager.SpawnBoard += Spawn;
        transform.position = new Vector3(10, -5, 0);
    }
    public void Spawn()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", GameCenter.position, "time", 1f, 
        "oncomplete", "AnimatePlayersSpawning", "oncompletetarget", gameAnimatorObject));
    }
}
