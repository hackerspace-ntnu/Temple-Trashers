using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManager : MonoBehaviour
{
    private static int playerNum = 0;
    private int playerIndex;
    private PlayerInput input;
    [SerializeField]
    private GameObject playerPrefab = null;
    private PlayerStateController instantiatedPlayer;
    public Vector3 spawnPoint;
    private void Awake()
    {
        playerIndex = playerNum;
        playerNum++;
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        spawnPoint = BaseController.Instance ? BaseController.Instance.SpawnPoint.position : Vector3.zero;
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        instantiatedPlayer = Instantiate(playerPrefab, spawnPoint, Quaternion.identity).GetComponent<PlayerStateController>();
        instantiatedPlayer.SetUpInput(input, this);
        Camera.main.GetComponent<CameraFocusController>().addFocusObject(instantiatedPlayer.transform);
    }
    public void RespawnPlayer(float delay)
    {
        StartCoroutine(waitForRespawn(delay));
    }
    private IEnumerator waitForRespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        InitializePlayer();
    }


}
