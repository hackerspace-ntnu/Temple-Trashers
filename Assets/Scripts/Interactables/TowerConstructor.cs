using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerConstructor : MonoBehaviour
{
    public Material hologramMaterial;
    private bool hologram = false;

    public List<Material[]> oldMaterials = new List<Material[]>();
    private SkinnedMeshRenderer[] skinmr;
    private MeshRenderer[] mr;

    // Component lists
    private List<MonoBehaviour> components = new List<MonoBehaviour>();
    private List<Animator> animators = new List<Animator>();

    private HexGrid terrain;
    private HexCell targetCell;

    public void BuildTurret()
    {
        // Construction assignments
        transform.position = targetCell.transform.position;
        targetCell.occupier = gameObject;

        // Visual changes
        ToggleHologram();

        // Activate all components
        foreach(MonoBehaviour m in components)
        {
            m.enabled = true;
        }
        foreach(Animator anim in animators)
        {
            anim.enabled = true;
        }

        // Clean up
        Destroy(this);
    }

    private void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        ToggleHologram();

        // Deactivate turret scripts
        foreach(MonoBehaviour c in GetComponentsInChildren(typeof(MonoBehaviour)))
        {
            if(c != this)
            {
                components.Add(c);
                c.enabled = false;
            }
        }
        foreach(Animator anim in GetComponentsInChildren(typeof(Animator)))
        {
            animators.Add(anim);
            anim.enabled = false;
        }

        // Find all materials and store them
        mr = GetComponentsInChildren<MeshRenderer>();
        skinmr = GetComponentsInChildren<SkinnedMeshRenderer>();

        for(int i = 0; i < skinmr.Length + mr.Length; i++)
        {
            if(i < mr.Length)
            {
                oldMaterials.Add(mr[i].materials);
            }
            else
            {
                oldMaterials.Add(skinmr[i - mr.Length].materials);
            }
        }
    }

    private void FixedUpdate()
    {
        targetCell = terrain.GetCell(transform.position + Vector3.forward * HexMetrics.outerRadius);
    }

    public void ToggleHologram()
    {
        // Turn off hologram material
        if (hologram)
        {
            for(int i = 0; i < oldMaterials.Count; i++)
            {
                if (i < mr.Length)
                {
                    mr[i].materials = oldMaterials[i];
                }
                else
                {
                    skinmr[i - mr.Length].materials = oldMaterials[i];
                }               
            }
            hologram = true;
        }
        else // Turn on hologram material
        {
            Material[] newMat = new Material[1];
            newMat[0] = hologramMaterial;
            for (int i = 0; i < oldMaterials.Count; i++)
            {
                if(i < mr.Length)
                {
                    mr[i].materials = newMat;
                }
                else
                {
                    skinmr[i - mr.Length].materials = newMat;
                }
            }
            hologram = true;
        }
    }
}
