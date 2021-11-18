using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTowerLogic : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public float bulletSpeed;
    public float bulletDamage;

    public void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.transform.SetParent(transform);
        BulletLogic newBulletLogic = newBullet.GetComponent<BulletLogic>();
        newBulletLogic.Damage = bulletDamage;
        newBulletLogic.BulletSpeed = bulletSpeed;
    }
}
