using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract = true;

    public virtual void Focus(PlayerStateController player)
    {
        Debug.Log("I am focused");
    }

    public virtual void Unfocus(PlayerStateController player)
    {
        Debug.Log("I am unfocused (Aren't we all?)");
    }

    public virtual void Interact(PlayerStateController player)
    {
        Debug.Log("Player interacted");
    }
}
