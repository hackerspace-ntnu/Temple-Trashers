using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTurretController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private MeshRenderer canOnPan;
    private Transform canTransform;

    [SerializeField]
    private GameObject canPrefab;

    [SerializeField]
    private Transform target;

    private GameObject canProjectile;
    private LavaCanController canProjectileController;


    

    void Start()
    {
        canTransform = canOnPan.transform;
    }

    public void Shoot()
    {
        canOnPan.enabled = false;
        //Do other shooty stuff
        ShootCan();
    }
    public void ResetCan()
    {
        canOnPan.enabled = true;
    }

    private void ShootCan()
    {
        if(canProjectile == null)
        {
            canProjectile = Instantiate(canPrefab, canTransform.position, canTransform.rotation);
            canProjectileController = canProjectile.GetComponent<LavaCanController>();
        }
        else
        {
            canProjectile.transform.position = canOnPan.transform.position;
            canProjectile.transform.rotation = canOnPan.transform.rotation;
            canProjectile.SetActive(true);
        }
        canProjectileController.Fly(target.position);
    }
    private void OnDestroy()
    {
        Destroy(canProjectile, 3f);
    }
}
