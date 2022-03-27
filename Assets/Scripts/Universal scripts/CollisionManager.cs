using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionManager : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();

    /// <summary>
    /// This manager is intended for custom mesh colliders and must be added to the GameObject that has the mesh collider component
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    public List<Collider> GetColliders()
    {
        //This is very stupid, but Unity has forced my hand.
        //This is due to Destroy() not triggering OnTriggerExit, requiring this manual check.
        colliders.RemoveAll(collider => collider == null);

        return colliders;
    }
}
