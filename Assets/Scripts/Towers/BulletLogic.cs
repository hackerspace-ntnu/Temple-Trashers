using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private float bulletSpeed;
    private float damage;

    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float Damage { get => damage; set => damage = value; }

    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    //On collision -> call collided objects damagelogic if it has one and destroy this bullet
    void OnTriggerEnter(Collider other)
    {
        HealthLogic healthLogic = other.GetComponent<HealthLogic>();
        if (healthLogic)
            healthLogic.DealDamage(damage);

        Destroy(gameObject);
    }

    void Update()
    {
        //move the bullet with specified speed
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }
}
