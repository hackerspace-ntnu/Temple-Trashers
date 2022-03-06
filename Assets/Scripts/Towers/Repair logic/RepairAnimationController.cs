using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RepairAnimationController : RepairController
{
    [SerializeField]
    private GameObject lowWearVFXPrefab;

    [SerializeField]
    private GameObject mediumWearVFXPrefab;

    [SerializeField]
    private GameObject highWearVFXPrefab;

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

    void OnDestroy()
    {
        onWearStateChange -= UpdateParticles;
    }

    private void DisableVFXInstances()
    {
        foreach (GameObject obj in new[] { lowWearVFX, mediumWearVFX, highWearVFX })
            obj.SetActive(false);
    }

    private void UpdateParticles(WearState newState, WearState previousState)
    {
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
