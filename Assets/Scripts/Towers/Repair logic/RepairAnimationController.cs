using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RepairAnimationController : RepairController
{
    [SerializeField]
    private GameObject lowWearVFXPrefab = default;

    [SerializeField]
    private GameObject mediumWearVFXPrefab = default;

    [SerializeField]
    private GameObject highWearVFXPrefab = default;

    [SerializeField]
    private GameObject explosionVFXPrefab = default;

    // Prefab instances
    private GameObject lowWearVFX;
    private GameObject mediumWearVFX;
    private GameObject highWearVFX;

    void Awake()
    {
        lowWearVFX = Instantiate(lowWearVFXPrefab, transform);
        mediumWearVFX = Instantiate(mediumWearVFXPrefab, transform);
        highWearVFX = Instantiate(highWearVFXPrefab, transform);
        DisableVFXInstances();
        onWearStateChange += UpdateParticles;
    }

    public new void Explode()
    {
        GameObject explosion = Instantiate(explosionVFXPrefab);
        explosion.GetComponent<LavaCanController>().timeToTarget = 0f;
        explosion.GetComponent<LavaCanController>().Fly(transform.position);
        Destroy(explosion, 5f);
        onWearStateChange -= UpdateParticles;
    }

    private void DisableVFXInstances()
    {
        foreach (GameObject obj in new[] { lowWearVFX, mediumWearVFX, highWearVFX })
            obj.SetActive(false);
    }

    private void UpdateParticles(WearState newState, WearState previousState)
    {
        if(previousState == WearState.HIGH && newState != WearState.NONE)
        {
            Explode();
        }

        switch (newState)
        {
            case WearState.NONE:
                DisableVFXInstances();
                break;
            case WearState.LOW:
                lowWearVFX.SetActive(true);
                break;
            case WearState.MEDIUM:
                mediumWearVFX.SetActive(true);
                break;
            case WearState.HIGH:
                highWearVFX.SetActive(true);
                break;
            default:
                DisableVFXInstances();
                break;
        }
    }
}
