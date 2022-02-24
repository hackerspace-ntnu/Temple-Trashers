using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletLogic : MonoBehaviour
{
    public float BulletSpeed { get; set; }
    public float Damage { get; set; }

    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        //move the bullet with specified speed
        transform.position += transform.forward * BulletSpeed * Time.deltaTime;
    }

    //On collision -> call collided objects damagelogic if it has one and destroy this bullet
    void OnTriggerEnter(Collider other)
    {
        HealthLogic healthLogic = other.GetComponent<HealthLogic>();
        if (healthLogic)
            healthLogic.DealDamage(Damage);

        Destroy(gameObject);
    }
}
