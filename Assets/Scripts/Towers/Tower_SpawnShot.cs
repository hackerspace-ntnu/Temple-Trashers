using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_SpawnShot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    void shoot()
    {
        GameObject newBullet = Instantiate(bullet, this.transform);
        //newBullet.setSpeed(bulletSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
