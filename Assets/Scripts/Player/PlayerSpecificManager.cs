using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSpecificManager : MonoBehaviour
{
    private static int playerCount = 0;
    private int playerIndex;
    private PlayerInput input;

    [SerializeField]
    private GameObject[] playerPrefabs = default;

    [SerializeField]
    private float respawnCost = 5f;


    private PlayerStateController instantiatedPlayer;
    public Vector3 spawnPoint;

    private Color playerColor;

    void Awake()
    {
        playerIndex = playerCount++;
        input = GetComponent<PlayerInput>();
        playerColor = Random.ColorHSV();
    }

    void Start()
    {
        InitializePlayer();

        UIManager.Singleton.DisableTutorial();
    }

    public void InitializePlayer()
    {
        // Get spawnpoint
        spawnPoint = BaseController.Singleton ? BaseController.Singleton.SpawnPoint.position : Vector3.zero;
        // Spawn at a random position around the base
        spawnPoint += new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));

        if (playerIndex > 2){ SteamManager.Singleton.SetAchievement("ACH_CROWD"); }
        GameObject playerToSpawn = playerPrefabs[playerIndex % playerPrefabs.Length];
        instantiatedPlayer = Instantiate(playerToSpawn, spawnPoint, playerToSpawn.transform.rotation).GetComponent<PlayerStateController>();
        instantiatedPlayer.SetUpInput(input, this, playerColor);
        CameraFocusController.Singleton.AddFocusObject(instantiatedPlayer.transform);

    }

    public void RespawnPlayer(float delay)
    {
        StartCoroutine(WaitForRespawn(delay));
        //Hurt the base for respawning a player (Damage is based on players lifetotal)
        BaseController.Singleton.HealthController.OnReceiveDamage(this, respawnCost);
    }

    private IEnumerator WaitForRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        InitializePlayer();
    }
}
