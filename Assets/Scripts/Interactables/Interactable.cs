using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool canInteract = true;

    public abstract void Focus(PlayerStateController player);

    public abstract void Unfocus(PlayerStateController player);

    public abstract void Interact(PlayerStateController player);
}
