using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    public float bulletSpeed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10.0f);
    }
    //Get-ers and Set-ers
    public void setSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }

    public float getSpeed()
    {
        return bulletSpeed;
    }

    public void setDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }
    public float getDamage()
    {
        return damage;
    }
    
    //On collision -> call collided objects damagelogic if it has one and destroy this bullet
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthLogic>())
        {
            other.gameObject.GetComponent<HealthLogic>().DealDamage(damage);
            
        }
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //move the bullet with specified speed
        transform.position += transform.forward *bulletSpeed * Time.deltaTime;
       
    }
}
