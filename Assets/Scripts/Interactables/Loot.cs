using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    // Selection / highlight material
    public Material selectionMaterial;

    // All mesh renderers attached to the object
    private MeshRenderer[] meshRenderers;

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

    private static readonly int stateMaterialProperty = Shader.PropertyToID("State");
    private static readonly int rateMaterialProperty = Shader.PropertyToID("Rate");

    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        inventory = InventoryManager.Singleton;
    }

    void Update()
    {
        // Destroy the loot properly (to be replaced with animation)
        if (destroy && !canInteract)
            DestroyAnimation();
    }

    private void DestroyAnimation()
    {
        transform.position = Vector3.Lerp(transform.position, absorbTarget, Time.deltaTime);
        if (transform.position.DistanceGreaterThan(0.7f, absorbTarget))
            return;

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            // Check if the material has the correct property
            if (!meshRenderer.material.HasProperty(stateMaterialProperty))
                continue;

            meshRenderer.material.SetFloat(stateMaterialProperty, dissolveState); // Continue animation
            baseController.ArcLengthVFX(transform, 1 - dissolveState);

            // If the animation is finished
            if (meshRenderers[0].material.GetFloat(stateMaterialProperty)
                / meshRenderers[0].material.GetFloat(rateMaterialProperty)
                > 1)
            {
                baseController.crystals++;
                baseController.RemoveRayVFX(transform, 10f);

                //Add resources to inventory
                inventory.ResourceAmount += lootValue;

                Destroy(gameObject);
            }
        }

        dissolveState += Time.deltaTime;
    }

    public override void Focus(PlayerStateController player)
    {
        Highlight();
    }

    public override void Unfocus(PlayerStateController player)
    {
        Unhighlight();
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

    private void Highlight()
    {
        // Add the selection material to all existing meshes
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            List<Material> rendererMaterials = new List<Material>(meshRenderer.materials);
            rendererMaterials.Add(selectionMaterial);
            meshRenderer.materials = rendererMaterials.ToArray();
        }
    }

    private void Unhighlight()
    {
        // Remove selection material from all meshes
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            Material[] rendererMaterials = meshRenderer.materials;
            Array.Resize(ref rendererMaterials, rendererMaterials.Length - 1);
            meshRenderer.materials = rendererMaterials;
        }
    }
}
