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
    private Vector3 absorbTarget = default;

    private BaseController baseController = default;

    // The current dissolve state
    private float dissolveState = 0;

    // The ray target
    public Transform target;

    //Loot-value to resources in inventory
    public int lootValue = 10;
    private InventoryManager inventory = default;

    // Loot Rigidbody
    private new Rigidbody rigidbody;

    // Loot collider
    private MeshCollider meshCollider = default;

    // Tutorial text
    [SerializeField]
    private TutorialText tutorialText = default;

    // Heal amount
    [SerializeField]
    private float baseHealAmount = 10f;

    private static readonly int stateMaterialProperty = Shader.PropertyToID("State");
    private static readonly int rateMaterialProperty = Shader.PropertyToID("Rate");

    void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        baseController = BaseController.Singleton;
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
                baseController.RemoveRayVFX(transform, 10f);

                //Add resources to inventory
                baseController.crystals++;
                inventory.ResourceAmount += lootValue;
                baseController.GetComponent<HealthLogic>().Heal(this, baseHealAmount);

                Destroy(gameObject);                
            }
        }

        dissolveState += Time.deltaTime;
    }

    public override void Focus(PlayerStateController player)
    {
        Highlight();
        tutorialText.Focus();
        tutorialText.SetButton(Direction.SOUTH, true);
    }

    public override void Unfocus(PlayerStateController player)
    {
        Unhighlight();
        tutorialText.Unfocus();
        tutorialText.SetButton(Direction.SOUTH, false);
    }

    public override void Interact(PlayerStateController player)
    {
        if (!carried)
        {
            // Carry the loot!
            carried = true;

            // Reset rigidbody
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

            // Disable collider
            meshCollider.enabled = false;

            player.Lift(gameObject);
        } else
        {
            // Drop the loot!
            carried = false;
            rigidbody.isKinematic = false;
            meshCollider.enabled = true;

            // If i'm to be destroyed, prevent me from being interacted with
            if (destroy)
            {
                canInteract = false;
                rigidbody.isKinematic = true;
            }

            player.Drop(gameObject);
            absorbTarget = transform.position + new Vector3(0, 3, 0);
        }
    }

    /// <summary>
    /// Prepares the loot object to be destroyed.
    /// </summary>
    public void Absorb()
    {
        // Set the object to be destroyed
        destroy = true;
        GetComponent<Collider>().enabled = false; //Prevents the vfx from being aborted when it leaves the base-sphere

        // Create flashing tutorialtext
        tutorialText.GetComponent<Animator>().enabled = true;
    }

    /// <summary>
    /// Prevents the object from being destroyed afterall, called when the loot leaves the base trigger zone.
    /// </summary>
    public void CancelAbsorb()
    {
        destroy = false;

        // Stop flashing ui
        tutorialText.GetComponent<Animator>().enabled = false;
    }

    /// <summary>
    /// Highlight the loot object
    /// </summary>
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

    /// <summary>
    /// Remove the highlight material
    /// </summary>
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
