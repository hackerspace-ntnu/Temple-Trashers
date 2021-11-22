using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerConstructor : MonoBehaviour
{
    public Material hologramMaterial;
    private bool hologram = false;

    public List<Material[]> oldMaterials = new List<Material[]>();
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private MeshRenderer[] meshRenderers;

    // Component lists
    private List<MonoBehaviour> components = new List<MonoBehaviour>();
    private List<Animator> animators = new List<Animator>();

    private HexGrid terrain;
    private HexCell targetCell;

    void Awake()
    {
        terrain = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();

        // Deactivate turret scripts
        foreach (MonoBehaviour component in GetComponentsInChildren<MonoBehaviour>())
        {
            if (component != this)
            {
                components.Add(component);
                component.enabled = false;
            }
        }

        foreach (Animator anim in GetComponentsInChildren<Animator>())
        {
            animators.Add(anim);
            anim.enabled = false;
        }

        // Find all materials and store them
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < skinnedMeshRenderers.Length + meshRenderers.Length; i++)
        {
            if (i < meshRenderers.Length)
                oldMaterials.Add(meshRenderers[i].materials);
            else
                oldMaterials.Add(skinnedMeshRenderers[i - meshRenderers.Length].materials);
        }
    }

    void Start()
    {
        ToggleHologram();
    }

    void FixedUpdate()
    {
        targetCell = terrain.GetCell(transform.position + Vector3.forward * HexMetrics.outerRadius);
    }

    public void BuildTurret()
    {
        // Construction assignments
        transform.position = targetCell.transform.position;
        targetCell.occupier = gameObject;

        // Visual changes
        ToggleHologram();

        // Activate all components
        foreach (MonoBehaviour component in components)
            component.enabled = true;

        foreach (Animator anim in animators)
            anim.enabled = true;

        // Clean up
        Destroy(this);
    }

    public void ToggleHologram()
    {
        // Turn off hologram material
        if (hologram)
        {
            for (int i = 0; i < oldMaterials.Count; i++)
            {
                if (i < meshRenderers.Length)
                    meshRenderers[i].materials = oldMaterials[i];
                else
                    skinnedMeshRenderers[i - meshRenderers.Length].materials = oldMaterials[i];
            }
        }
        // Turn on hologram material
        else
        {
            Material[] newMat = { hologramMaterial };
            for (int i = 0; i < oldMaterials.Count; i++)
            {
                if (i < meshRenderers.Length)
                    meshRenderers[i].materials = newMat;
                else
                    skinnedMeshRenderers[i - meshRenderers.Length].materials = newMat;
            }

            hologram = true;
        }
    }
}
