using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();

    /// <summary>
    /// This manager is intended for custom mesh colliders and must be added to the GameObject that has the mesh collider component
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    public List<Collider> getColliders()
    {
        //This is very stupid, but Unity has forced my hand.
        //This is due to Destroy() not triggering OnTriggerExit, requiring this manual check.
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i] == null){colliders.RemoveAt(i);}
        }
        return colliders;
    }
    public Collider getCollider(int index)
    {
        try
        {
            return colliders[index];
        }
        catch
        {
            return null;
        }
        
    }
}
