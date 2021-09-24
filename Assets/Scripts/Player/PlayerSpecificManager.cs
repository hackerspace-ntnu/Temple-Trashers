using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpecificManager : MonoBehaviour
{
    private static int playerNum = 0;
    private int playerIndex;
    private PlayerInput input;

    [SerializeField]
    private GameObject playerPrefab;

    private PlayerStateController instantiatedPlayer;
    public Vector3 spawnPoint;

    void Awake()
    {
        playerIndex = playerNum;
        playerNum++;
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        spawnPoint = BaseController.Singleton ? BaseController.Singleton.SpawnPoint.position : Vector3.zero;
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        instantiatedPlayer = Instantiate(playerPrefab, spawnPoint, Quaternion.identity).GetComponent<PlayerStateController>();
        instantiatedPlayer.SetUpInput(input, this);
        CameraFocusController.Singleton.AddFocusObject(instantiatedPlayer.transform);
    }

    public void RespawnPlayer(float delay)
    {
        StartCoroutine(WaitForRespawn(delay));
    }

    private IEnumerator WaitForRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        InitializePlayer();
    }
}
