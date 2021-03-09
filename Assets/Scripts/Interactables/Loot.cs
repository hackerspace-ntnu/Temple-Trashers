using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    private MeshRenderer mr;
    [ReadOnly]
    public bool carried = false;
    private bool destroy = false;
    private Vector3 absorbTarget; // The position to be absorbed in

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public override void Interact(PlayerStateController player)
    {
        StartCoroutine(SelectDelay(0.5f));
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

    private IEnumerator SelectDelay(float delay)
    {
        mr.materials[mr.materials.Length - 1].SetVector("HoloColor", Color.red);
        yield return new WaitForSeconds(delay);
        mr.materials[mr.materials.Length - 1].SetVector("HoloColor", selectionMaterial.GetVector("HoloColor"));
    }

    public void Absorb()
    {
        // Set the object to be destroyed
        destroy = true;
        absorbTarget = transform.position + new Vector3(0, 3, 0);        
    }

    private void Update()
    {
        if (destroy && !canInteract)
        {
            transform.position = Vector3.Lerp(transform.position, absorbTarget, Time.deltaTime);
            if(Vector3.Distance(transform.position, absorbTarget) < 0.3f)
            {
                mr.material.SetFloat("State", mr.material.GetFloat("State") + Time.deltaTime);
                if (mr.material.GetFloat("State") / mr.material.GetFloat("Rate") > 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
