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

    // Start is called before the first frame update
    void Start()
    {
        HexGrid hexGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        Vector3 v = hexGrid.GetCell(transform.position).transform.position;
        transform.position = v;
    }
    private void FixedUpdate()
    {
        ChangeDirection();
    }

    // Allow turret to be operated when focused
    public override void Focus(PlayerStateController player)
    {
        input = player.GetComponent<TurretInput>();

        //render arrowPointer
        arrowPointer.GetComponent<Renderer>().enabled = true;
    }

    // When player leaves, prevent it from changing the turret position
    public override void Unfocus(PlayerStateController player)
    {
        input = null;

        //unrender pointer
        arrowPointer.GetComponent<Renderer>().enabled = false;
    }

    //Rotational-Movement using UserInput
    void ChangeDirection()
    {
        if (input)
        {
            Vector2 aim = input.GetAimInput();
            if (aim.sqrMagnitude > 0.01f)
            {
                float angle = -Mathf.Atan2(aim.y, aim.x) * 180 / Mathf.PI;// - 90;
                rotAxis.rotation = Quaternion.Euler(0f, angle, 0f);
                Quaternion.RotateTowards(towerRotationPoint.transform.rotation, rotAxis.rotation, rotationMaxSpeed * Time.deltaTime);
            }
        }
    }

    void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.GetComponent<BulletLogic>().SetDamage(bulletDamage);
        newBullet.GetComponent<BulletLogic>().SetSpeed(bulletSpeed);
        newBullet.transform.SetParent(this.transform);
    }
   
}
