using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxeTowerAnimationController : MonoBehaviour, TurretInterface
{
    [SerializeField]
    private MeshRenderer handAxe = default;

    [SerializeField]
    private MeshRenderer totemAxe = default;

    [SerializeField]
    private Transform spawnPoint = default;

    [SerializeField]
    private GameObject axeProjectilePrefab = default;

    [SerializeField]
    private float projectileSpeed = 5f;

    [SerializeField]
    private Transform forwardTransform = default;

    [SerializeField]
    private AudioSource throws = default;

    [SerializeField]
    private AudioSource hits = default;

    public void Hit()
    {
        hits.Play();
    }

    public void Grab()
    {
        handAxe.enabled = true;
        totemAxe.enabled = false;
    }

    public void Shoot()
    {
        handAxe.enabled = false;
        Rigidbody projectileBody = Instantiate(axeProjectilePrefab, spawnPoint.position, spawnPoint.rotation, transform)
            .GetComponent<Rigidbody>();
        projectileBody.velocity = forwardTransform.right * projectileSpeed;
        throws.Play();
    }

    public void Spawn()
    {
        totemAxe.enabled = true;
    }
}
