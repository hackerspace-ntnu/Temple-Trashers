using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
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
        
        //instantiatedPlayer.GetComponent<HealthLogic>().onDeath += 
        
        CameraFocusController.Singleton.AddFocusObject(instantiatedPlayer.transform);

    }

    public void StartRespawnPlayer(float delay)
    {
        StartCoroutine(WaitForRespawn(delay));
        //Hurt the base for respawning a player (Damage is based on players lifetotal)
        BaseController.Singleton.HealthController.OnReceiveDamage(this, respawnCost);
    }

    public void RespawnPlayer()
    {
        GameObject playerToSpawn = playerPrefabs[playerIndex % playerPrefabs.Length];
        instantiatedPlayer = Instantiate(playerToSpawn, spawnPoint, playerToSpawn.transform.rotation).GetComponent<PlayerStateController>();
        instantiatedPlayer.SetUpInput(input, this, playerColor);
    }

    private IEnumerator WaitForRespawn(float delay)
    {
        //Create new spawnpoint
        spawnPoint = BaseController.Singleton ? BaseController.Singleton.SpawnPoint.position : Vector3.zero;
        spawnPoint += new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));

        // Delay
        yield return new WaitForSeconds(delay);

        instantiatedPlayer.ReturnPlayerToSpawn();
        instantiatedPlayer.GetComponent<PlayerRagdollController>().UnRagdoll();
    }
}
