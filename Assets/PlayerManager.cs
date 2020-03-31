using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public GameObject PlayerHuman;
    public GameObject PlayerAI;



    public void SpawnPlayers() {
        PlayerHuman.SetActive(true);
        PlayerAI.SetActive(true);

        PlayerHuman.GetComponent<Player>().SpawnPlayer();
        PlayerAI.GetComponent<Player>().SpawnPlayer();
    }



    private void Update()
    {
        if (Input.anyKeyDown) {
            PlayerHuman.GetComponent<Player>().MovePlayer(Random.Range(2,20));
        }

    }
}
