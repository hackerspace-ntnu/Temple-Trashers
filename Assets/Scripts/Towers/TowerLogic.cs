using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : Interactable
{
    public Vector3 lookingTowards;
    public GameObject bulletSpawnPoint;
    public GameObject towerRotationPoint;

    public GameObject arrowPointer;

    //public bool autoShoot;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDamage;
    public TurretInput input;
    public Transform rotAxis;
    public float rotationMaxSpeed;

    private Renderer arrowPointerRenderer;

    void Start()
    {
        HexGrid hexGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        Vector3 cellPos = hexGrid.GetCell(transform.position).transform.position;
        transform.position = cellPos;

        // Some towers might not use an arrow pointer
        arrowPointerRenderer = arrowPointer ? arrowPointer.GetComponent<Renderer>() : null;
    }

    void FixedUpdate()
    {
        ChangeDirection();
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        input = player.GetComponent<TurretInput>();

        //render arrowPointer
        if (arrowPointerRenderer)
            arrowPointerRenderer.enabled = true;
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        input = null;

        //unrender pointer
        if (arrowPointerRenderer)
            arrowPointerRenderer.enabled = false;
    }

    //Rotational-Movement using UserInput
    private void ChangeDirection()
    {
        if (!input)
            return;

        Vector2 aim = input.GetAimInput();
        if (aim.sqrMagnitude > 0.01f)
        {
            float angle = -Mathf.Atan2(aim.y, aim.x) * 180 / Mathf.PI; // - 90;
            rotAxis.rotation = Quaternion.Euler(0f, angle, 0f);
            Quaternion.RotateTowards(towerRotationPoint.transform.rotation, rotAxis.rotation, rotationMaxSpeed * Time.deltaTime);
        }
    }

    private void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.transform.SetParent(transform);
        BulletLogic newBulletLogic = newBullet.GetComponent<BulletLogic>();
        newBulletLogic.SetDamage(bulletDamage);
        newBulletLogic.SetSpeed(bulletSpeed);
    }
}
