using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component made for updating the player characters' props when states are changed.
/// Latches on to <c>PlayerStateContoller</c>'s <c>onPlayerStateChange</c> delegate to enable/disabled the crate and blueprints.
/// </summary>
public class PlayerPropController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer crate = default;

    [SerializeField]
    private MeshRenderer blueprint = default;

    private PlayerStateController playerStateController;

    void Awake()
    {
        playerStateController = GetComponent<PlayerStateController>();
        playerStateController.onPlayerStateChange += OnPlayerStateChange;
    }

    void OnDestroy()
    {
        playerStateController.onPlayerStateChange -= OnPlayerStateChange;
    }

    private void OnPlayerStateChange(PlayerStates newState, PlayerStates oldState)
    {
        switch (oldState)
        {
            case PlayerStates.BUILDING:
                crate.enabled = false;
                break;
            case PlayerStates.IN_TURRET_MENU:
                blueprint.enabled = false;
                break;
        }

        switch (newState)
        {
            case PlayerStates.BUILDING:
                crate.enabled = true;
                break;
            case PlayerStates.IN_TURRET_MENU:
                blueprint.enabled = true;
                break;
        }
    }
}
