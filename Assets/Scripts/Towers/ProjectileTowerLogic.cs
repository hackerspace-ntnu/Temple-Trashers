using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileTowerLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = default;

    [SerializeField]
    private GameObject bulletSpawnPoint = default;

    [SerializeField]
    private float bulletSpeed = default;

    [SerializeField]
    private float bulletDamage = default;

    public void LaunchProjectile()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        newBullet.transform.SetParent(transform);
        BulletLogic newBulletLogic = newBullet.GetComponent<BulletLogic>();
        newBulletLogic.Damage = bulletDamage;
        newBulletLogic.BulletSpeed = bulletSpeed;
    }
}
