using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour
{
    public Vector3 lookingTowards;
    public GameObject bulletSpawnPoint;
    //public bool autoShoot;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("LaunchProjectile", 2.0f, 1f);
    }

    // Update is called once per frame
    void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.GetComponent<BulletLogic>().setDamage(bulletDamage);
    }
    void Update()
    {
    }
}
