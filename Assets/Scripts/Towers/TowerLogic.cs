using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    public Vector3 lookingTowards;
    public GameObject bulletSpawnPoint;
    public GameObject towerRotationPoint;
    public GameObject arrowPointer;
    //public bool autoShoot;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDamage;
    public UserInput input;
    public Transform rotAxis;
    public float rotationMaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        HexGrid hexGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<HexGrid>();
        Vector3 v = new Vector3(hexGrid.GetCell(transform.position).transform.position.x, 0, hexGrid.GetCell(transform.position).transform.position.z);
        transform.position = v;
    }
    private void FixedUpdate()
    {
        changeDirection();
    }
    //Insert UserInput of nearby player
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UserInput>())
        { 
            input = other.GetComponent<UserInput>();

            //render arrowPointer
            arrowPointer.GetComponent<Renderer>().enabled = true;
        }
    }
    //remove UserInput if user leaves radius
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<UserInput>() == input)
        {
            input = null;

            //unrender pointer
            arrowPointer.GetComponent<Renderer>().enabled = false;
        }
    }


    //Rotational-Movement using UserInput
    void changeDirection()
    {
        if (input)
        {
            float angle = Mathf.Atan2(input.AimInput.y, input.AimInput.x) * 180 - 90 / Mathf.PI;

            if(angle > 0.01f)
            {
            rotAxis.rotation = Quaternion.Euler(0f, angle, 0f);
                Quaternion.RotateTowards(towerRotationPoint.transform.rotation, rotAxis.rotation, rotationMaxSpeed * Time.deltaTime);
                //towerRotationPoint.transform.rotation = rotAxis.rotation;
            }
        }
    }

    void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.GetComponent<BulletLogic>().setDamage(bulletDamage);
        newBullet.GetComponent<BulletLogic>().setSpeed(bulletSpeed);
        newBullet.transform.SetParent(this.transform);
    }
   
}
