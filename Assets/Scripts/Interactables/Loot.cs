using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    // Selection / highlight material
    public Material selectionMaterial;

    // All mesh renderers attached to the object
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    [ReadOnly]
    // Is the object being carried
    public bool carried = false;

    // Set the object to be destroyed
    private bool destroy = false;

    // The position to be absorbed in
    private Vector3 absorbTarget;

    BaseController baseController;

    // The current dissolve state
    private float dissolveState = 0;

    // The ray target
    public Transform target;

    //Loot-value to resources in inventory
    public int lootValue = 10;
    private InventoryManager inventory;

    private Material[] newMat;

    void Start()
    {
        foreach (MeshRenderer childRenderer in GetComponentsInChildren<MeshRenderer>())
            meshRenderers.Add(childRenderer);

        inventory = InventoryManager.Singleton;
    }

    void Update()
    {
        // Destroy the loot properly (to be replaced with animation)
        if (destroy && !canInteract)
        {
            transform.position = Vector3.Lerp(transform.position, absorbTarget, Time.deltaTime);
            if (Vector3.Distance(transform.position, absorbTarget) < 0.7f)
            {
                for (int y = 0; y < meshRenderers.Count; y++)
                {
                    // Check if list item is empty
                    if (meshRenderers[y] != null)
                    {
                        // Check if the material has the correct property
                        if (meshRenderers[y].material.HasProperty("State"))
                        {
                            meshRenderers[y].material.SetFloat("State", dissolveState); // Continue animation
                            baseController.ArcLengthVFX(transform, 1 - dissolveState);

                            // If the animation is finished
                            if (meshRenderers[0].material.GetFloat("State") / meshRenderers[0].material.GetFloat("Rate") > 1)
                            {
                                baseController.crystals++;
                                baseController.RemoveRayVFX(transform, 10f);

                                //Add resources to inventory
                                inventory.ResourceAmount += lootValue;

                                Destroy(gameObject);
                            }
                        }
                    } else
                    {
                        meshRenderers.RemoveAt(y);
                        return;
                    }
                }

                dissolveState += Time.deltaTime;
            }
        }
    }

    public override void Interact(PlayerStateController player)
    {
        if (!carried)
        {
            // Carry the loot!
            carried = true;
            player.Lift(gameObject);
        } else
        {
            // Drop the loot!
            carried = false;
            // If i'm to be destroyed, prevent me from being interacted with
            if (destroy)
                canInteract = false;

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

    public void Absorb(BaseController baseController)
    {
        // Set the object to be destroyed
        this.baseController = baseController;
        destroy = true;
        GetComponent<Collider>().enabled = false; //Prevents the vfx from being aborted when it leaves the base-sphere
    }

    public void CancelAbsorb()
    {
        destroy = false;
    }

    public void Highlight()
    {
        // Add the hologram material on all existing meshes
        for (int y = 0; y < meshRenderers.Count; y++)
        {
            if (meshRenderers[y] != null)
            {
                newMat = new Material[meshRenderers[y].materials.Length + 1];
                for (int i = 0; i < newMat.Length; i++)
                {
                    if (i < newMat.Length - 1)
                        newMat[i] = meshRenderers[y].materials[i];
                    else
                        newMat[i] = selectionMaterial;

                    meshRenderers[y].materials = newMat;
                }
            }
        }
    }

    public void Unhighlight()
    {
        // Remove selection material from all meshes
        for (int y = 0; y < meshRenderers.Count; y++)
        {
            if (meshRenderers[y] != null)
            {
                newMat = new Material[meshRenderers[y].materials.Length - 1];
                for (int i = 0; i < newMat.Length; i++)
                    newMat[i] = meshRenderers[y].materials[i];

                meshRenderers[y].materials = newMat;
            }
        }
    }
}
