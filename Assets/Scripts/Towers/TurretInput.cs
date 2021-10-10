using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInput : MonoBehaviour
{
    public GameObject tower;

    private IEnumerator coroutine;
    private PlayerStateController player;
    private HexGrid hexGrid;

    void Start()
    {
        hexGrid = GameObject.Find("Terrain").GetComponent<HexGrid>();
        player = GetComponent<PlayerStateController>();

        coroutine = WaitAndPrint(5.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //PlaceTower(tower, transform.position);
        }
    }

    public Vector2 GetAimInput()
    {
        if (player.CurrentState == PlayerStateController.PlayerStates.DEAD)
            return Vector2.zero;

        return player.AimInput;
    }

    public PlayerStateController.PlayerStates CurrentPlayerState()
    {
        return player.CurrentState;
    }

    public void PlaceTower(GameObject tower, Vector3 position)
    {
        HexCell cell = hexGrid.GetCell(position);
        cell.isOccupied = true;
        cell.occupier = Instantiate(tower, cell.transform);
    }
}
