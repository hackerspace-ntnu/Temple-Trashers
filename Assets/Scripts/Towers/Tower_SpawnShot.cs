using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_SpawnShot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform);
        newBullet.GetComponent<BulletLogic>().BulletSpeed = bulletSpeed;
    }
}
