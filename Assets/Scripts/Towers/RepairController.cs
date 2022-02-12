using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairController : MonoBehaviour
{
    [SerializeField]
    private float damageInterval;

    [SerializeField]
    private HealthLogic healthLogic;

    [SerializeField]
    private int durability;

    private int wearDamage;

    private float timeStamp;
    private bool needRepair = false;

    public void Repair(){
        needRepair = false;
    }

    void Start()
    {
        timeStamp = Time.deltaTime;    
    }

    void Update()
    {
        if (needRepair) { healthLogic.DealDamage(wearDamage); }
        if (timeStamp < Time.deltaTime + damageInterval){
            timeStamp = Time.deltaTime;
            needRepair = true;
        }
    }
}
