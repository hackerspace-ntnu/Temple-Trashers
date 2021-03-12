using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    public Material selectionMaterial;  // Selection / highlight material
    private List<MeshRenderer> mr = new List<MeshRenderer>();          // All mesh renderers attached to the object
    [ReadOnly]
    public bool carried = false;        // Is the object being carried
    private bool destroy = false;       // Set the object to be destroyed
    private Vector3 absorbTarget;       // The position to be absorbed in
    BaseController b;                   // The base
    private float dissolveState = 0;    // The current dissolve state

    private void Start()
    {
        mr.Add(GetComponent<MeshRenderer>());
        foreach(MeshRenderer childRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            mr.Add(childRenderer);
        }
    }

    public override void Interact(PlayerStateController player)
    {
        if (!carried)
        {
            // Carry the loot!
            carried = true;
            player.Lift(gameObject);
            transform.SetParent(player.transform);
            transform.position = player.inventory.position;
        }
        else
        {
            // Drop the loot!
            carried = false;
            transform.parent = null;

            // If i'm to be destroyed, prevent me from being interacted with
            if (destroy) 
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

    public void Absorb(BaseController b)
    {
        // Set the object to be destroyed
        this.b = b;
        destroy = true;        
    }

    public void CancelAbsorb()
    {
        destroy = false;
    }

    private void Update()
    {
        if (destroy && !canInteract) // Destroy the loot properly (to be replaced with animation)
        {
            transform.position = Vector3.Lerp(transform.position, absorbTarget, Time.deltaTime);
            if (Vector3.Distance(transform.position, absorbTarget) < 0.3f)
            {
                for(int y = 0; y < mr.Count; y++)
                {
                    if (mr[y] != null) // Check if list item is empty
                    {
                        if (mr[y].material.HasProperty("State")) // Check if the material has the correct property
                        {
                            mr[y].material.SetFloat("State", dissolveState); // Continue animation

                            if (mr[0].material.GetFloat("State") / mr[0].material.GetFloat("Rate") > 1) // If the animation is done finish.
                            {
                                b.crystals++;
                                Destroy(gameObject);
                            }
                        }
                    }
                    else
                    {
                        mr.RemoveAt(y);
                        return;
                    }
                }
                dissolveState += Time.deltaTime;
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
