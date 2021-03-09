using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Material selectionMaterial;
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

    public void Highlight()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        // Apply the hologram material on all existing meshes
        Material[] newMat = new Material[mr.materials.Length + 1];
        for (int i = 0; i < newMat.Length; i++)
        {
            if (i < newMat.Length - 1)
            {
                newMat[i] = mr.materials[i];
            }
            else
            {
                newMat[i] = selectionMaterial;
            }
            mr.materials = newMat;
        }
    }

    public void Unhighlight()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        // Remove selection material from all meshes
        Material[] newMat = new Material[mr.materials.Length - 1];
        for (int i = 0; i < newMat.Length; i++)
        {
            newMat[i] = mr.materials[i];
        }
        mr.materials = newMat;
    }
}
