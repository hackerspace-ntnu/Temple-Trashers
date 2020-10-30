using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    public static PlayerRespawnController Instance;

    public Transform respawnPoint;
    public float respawnTime;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void waitForRespawn(PlayerStateController player)
    {
        StartCoroutine(spawnPlayerWithDelay(player));
    }
    private IEnumerator spawnPlayerWithDelay(PlayerStateController player)
    {
        //Die the player

        yield return new WaitForSeconds(respawnTime);
        player.transform.position = respawnPoint.position;
        player.revive();
        //Un-death the player
    }
}
