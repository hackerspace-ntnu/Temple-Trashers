using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component made for updating dahls props when states are changed. Latches on to playerStateContollers onStateChange delegate to enable/disabled the crate and blueprints
/// </summary>
public class DahlPropController : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer Crate, Blueprint;
    void Start()
    {
        GetComponent<PlayerStateController>().onPlayerStateChange += onPlayerStateChange;
    }
    private void OnDestroy()
    {
        GetComponent<PlayerStateController>().onPlayerStateChange -= onPlayerStateChange;
    }

    private void onPlayerStateChange(PlayerStateController.PlayerStates newState, PlayerStateController.PlayerStates oldState)
    {
        switch (oldState)
        {
            case PlayerStateController.PlayerStates.BUILDING:
                Crate.enabled = false;
                break;
            case PlayerStateController.PlayerStates.IN_TURRET_MENU:
                Blueprint.enabled = false;
                break;
            default:
                break;
        }

        switch (newState)
        {
            case PlayerStateController.PlayerStates.BUILDING:
                Crate.enabled = true;
                break;
            case PlayerStateController.PlayerStates.IN_TURRET_MENU:
                Blueprint.enabled = true;
                break;
            default:
                break;
        }

        
    }
}  
