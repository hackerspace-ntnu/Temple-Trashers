using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTowerController : MonoBehaviour
{
    public MeshRenderer handAxe, totemAxe;
    public Transform spawnPoint;
    public GameObject axeProjectilePrefab;
    public float projectileSpeed = 5f;
    public Transform forwardTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void grab()
    {
        handAxe.enabled = true;
        totemAxe.enabled = false;
    }
    public void toss()
    {
        handAxe.enabled = false;
        Rigidbody projectileBody =  Instantiate(axeProjectilePrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Rigidbody>();
        projectileBody.velocity = forwardTransform.right * projectileSpeed;
    }
    public void spawn()
    {
        totemAxe.enabled = true;
    }
}
