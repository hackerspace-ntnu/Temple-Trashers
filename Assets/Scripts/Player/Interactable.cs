using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public virtual void focus(PlayerStateController player)
    {
        Debug.Log("I am focused");
    }

    public virtual void unFocus(PlayerStateController player)
    {
        Debug.Log("I am unfocused (Aren't we all?)");
    }

    public virtual void interact(PlayerStateController player)
    {
        Debug.Log("Player interacted");
    }
}
