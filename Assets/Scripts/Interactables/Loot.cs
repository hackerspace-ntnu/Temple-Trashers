using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    public Material selectionMaterial;  // Selection / highlight material
    private List<MeshRenderer> mr = new List<MeshRenderer>();          // All mesh renderers attached to the object
    [ReadOnly]
    public bool carried = false;    // Is the object being carried
    private bool destroy = false;   // Set the object to be destroyed
    private Vector3 absorbTarget; // The position to be absorbed in

    private void Start()
    {
        mr.Add(GetComponent<MeshRenderer>());
    }

    public override void Interact(PlayerStateController player)
    {
        if (!carried)
        {
            // Carry the loot!
            carried = true;
            player.Lift(gameObject);
            transform.SetParent(player.transform);
            Transform[] children = player.GetComponentsInChildren<Transform>();
            foreach(Transform t in children)
            {
                if (t.name == "Luggage")
                {
                    transform.position = t.position;
                    return;
                }
            }
        }
        else
        {
            // Drop the loot!
            carried = false;
            transform.parent = null;
            if (destroy) // If i'm to be destroyed, prevent me from being interacted with
            {
                canInteract = false;
            }
            player.Drop(gameObject);
            absorbTarget = transform.position + new Vector3(0, 3, 0); 
        }
    }

    public override void Focus(PlayerStateController player)
    {
        Highlight();
    }

    public override void Unfocus(PlayerStateController player)
    {
        Unhighlight();
    }

    public void Absorb()
    {
        // Set the object to be destroyed
        destroy = true;        
    }

    public void CancelAbsorb()
    {
        destroy = false;
    }

    private void Update()
    {
        if (destroy && !canInteract) // Destroy the loot with animation
        {
            transform.position = Vector3.Lerp(transform.position, absorbTarget, Time.deltaTime);
            if(Vector3.Distance(transform.position, absorbTarget) < 0.3f)
            {
                for(int y = 0; y < mr.Count; y++)
                {
                    mr[y].material.SetFloat("State", mr[y].material.GetFloat("State") + Time.deltaTime);
                }
                if (mr[0].material.GetFloat("State") / mr[0].material.GetFloat("Rate") > 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Highlight()
    {
        // Apply the hologram material on all existing meshes
        for(int y = 0; y < mr.Count; y++)
        {
            if(mr[y] != null)
            {
                Material[] newMat = new Material[mr[y].materials.Length + 1];
                for (int i = 0; i < newMat.Length; i++)
                {
                    if (i < newMat.Length - 1)
                    {
                        newMat[i] = mr[y].materials[i];
                    }
                    else
                    {
                        newMat[i] = selectionMaterial;
                    }
                    mr[y].materials = newMat;
                }
            }
        }
        
    }

    public void Unhighlight()
    {
        // Remove selection material from all meshes
        for(int y = 0; y < mr.Count; y++)
        {
            if (mr[y] != null)
            {
                Material[] newMat = new Material[mr[y].materials.Length - 1];
                for (int i = 0; i < newMat.Length; i++)
                {
                    newMat[i] = mr[y].materials[i];
                }
                mr[y].materials = newMat;
            }
        }
    }

    
}
