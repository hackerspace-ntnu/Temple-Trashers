using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower_SpawnShot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = default;

    [SerializeField]
    private float bulletSpeed = default;

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform);
        newBullet.GetComponent<BulletLogic>().BulletSpeed = bulletSpeed;
    }
}
