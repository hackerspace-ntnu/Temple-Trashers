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
    public TurretInput input;
    public Transform rotAxis;
    public float rotationMaxSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        changeDirection();
    }
    //Insert UserInput of nearby player
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TurretInput>())
        { 
            input = other.GetComponent<TurretInput>();

            //render arrowPointer
            arrowPointer.GetComponent<Renderer>().enabled = true;
        }
    }
    //remove UserInput if user leaves radius
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TurretInput>() == input)
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
            
            Vector2 aim = input.GetAimInput();
            if (aim.sqrMagnitude > 0.01f)
            {
                float angle = -Mathf.Atan2(aim.y, aim.x) * 180 / Mathf.PI;// - 90;
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
        newBullet.transform.SetParent(this.transform);
    }
   
}
