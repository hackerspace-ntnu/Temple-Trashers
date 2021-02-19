using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretInput : MonoBehaviour
{

    private IEnumerator coroutine;
    PlayerStateController player;
    HexGrid hexGrid;
    public GameObject tower;
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);

            PlaceTower(tower, transform.position);
        }
    }
    void Start()
    {
        hexGrid = GameObject.Find("Terrain").GetComponent<HexGrid>();
        player = GetComponent<PlayerStateController>();

        coroutine = WaitAndPrint(5.0f);
        StartCoroutine(coroutine);


    }

    public Vector2 GetAimInput()
    {
        if(player.CurrentState == PlayerStateController.PlayerStates.Dead)
        {
            return Vector2.zero;
        }
        return player.AimInput;
    }
    public PlayerStateController.PlayerStates CurrentPlayerState()
    {
        return player.CurrentState;
    }
    public void PlaceTower(GameObject tower, Vector3 position) {
        HexCell cell = hexGrid.GetCell(position);
        cell.isOccupied = true;
        cell.occupier = Instantiate(tower, cell.transform.position, cell.transform.rotation);
    }
}
