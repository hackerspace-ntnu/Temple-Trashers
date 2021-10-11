using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component made for updating Dahl's props when states are changed.
/// Latches on to `PlayerStateContoller`s `onPlayerStateChange` delegate to enable/disabled the crate and blueprints.
/// </summary>
public class DahlPropController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer crate;

    [SerializeField]
    private MeshRenderer blueprint;

    void Start()
    {
        GetComponent<PlayerStateController>().onPlayerStateChange += OnPlayerStateChange;
    }

    void OnDestroy()
    {
        GetComponent<PlayerStateController>().onPlayerStateChange -= OnPlayerStateChange;
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
            default:
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
            default:
                break;
        }
    }
}
