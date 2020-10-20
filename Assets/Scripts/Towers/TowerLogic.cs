using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    public Vector3 lookingTowards;
    public GameObject bulletSpawnPoint;
    public GameObject towerRotationPoint;
    //public bool autoShoot;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDamage;
    public UserInput input;
    public Transform rotAxis;

    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("LaunchProjectile", 2.0f, 1f);
    }
    private void FixedUpdate()
    {
        changeDirection();
    }
    //Movement
    void changeDirection()
    {/*
        float angle = Mathf.Atan2(input.AimInput.y, input.AimInput.x)*360/Mathf.PI;
        rotAxis.rotation = Quaternion.Euler(0f, angle, 0f);
        towerRotationPoint.transform.rotation = rotAxis.rotation;*/
    }

    // Update is called once per frame
    void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.GetComponent<BulletLogic>().setDamage(bulletDamage);
    }

    public void enterTurret()
    {
        this.active = true;
        Debug.Log("Entered turret");
    }
    public void exitTurret()
    {
        this.active = false;
        Debug.Log("Left turret");
    }

    private Vector3 rotationSpeed = new Vector3(0, 200f, 0);
    void Update()
    {
        if(this.active) // If the tower is "active" (The player enters the tower)
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(-this.rotationSpeed*Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(this.rotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if(active)
            {
                this.exitTurret();
            } else
            {
                this.enterTurret();
            }
        }
    }
}
