using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    public bool canInteract = true;

    public virtual void Focus(PlayerStateController player)
    {}

    public virtual void Unfocus(PlayerStateController player)
    {}

    public abstract void Interact(PlayerStateController player);
}
