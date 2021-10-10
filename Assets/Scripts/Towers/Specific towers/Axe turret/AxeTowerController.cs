using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTowerController : Interactable, TurretInterface
{
    public MeshRenderer handAxe;
    public MeshRenderer totemAxe;
    public Transform spawnPoint;
    public GameObject axeProjectilePrefab;
    public float projectileSpeed = 5f;
    public Transform forwardTransform;

    [SerializeField]
    private Transform rotationAxis;

    private bool focused = false;
    private PlayerStateController playerAiming;

    void FixedUpdate()
    {
        if (focused)
        {
            Vector2 dir = playerAiming.AimInput;
            if (dir != Vector2.zero)
            {
                float angle = -Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
                rotationAxis.rotation = Quaternion.Euler(new Vector3(-90f, angle, 0f));
            }
        }
    }

    public override void Unfocus(PlayerStateController player)
    {
        if (player == playerAiming)
        {
            focused = false;
            playerAiming = null;
        }
    }

    public override void Focus(PlayerStateController player)
    {
        focused = true;
        playerAiming = player;
    }

    public void Grab()
    {
        handAxe.enabled = true;
        totemAxe.enabled = false;
    }

    public void Shoot()
    {
        handAxe.enabled = false;
        Rigidbody projectileBody = Instantiate(axeProjectilePrefab, spawnPoint.position, spawnPoint.rotation)
            .GetComponent<Rigidbody>();
        projectileBody.velocity = forwardTransform.right * projectileSpeed;
    }

    public void Spawn()
    {
        totemAxe.enabled = true;
    }
}
