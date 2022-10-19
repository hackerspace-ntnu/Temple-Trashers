using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LavaTurretController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer canOnPan = default;

    [SerializeField]
    private GameObject canPrefab = default;

    [SerializeField]
    private Transform target = default;

    private Transform canTransform;
    private GameObject canProjectile;
    private LavaCanController canProjectileController;

    void Start()
    {
        canTransform = canOnPan.transform;
    }

    void OnDestroy()
    {
        Destroy(canProjectile, 3f);
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
        if (canProjectile == null)
        {
            canProjectile = Instantiate(canPrefab, canTransform.position, canTransform.rotation, transform);
            canProjectileController = canProjectile.GetComponent<LavaCanController>();
        } else
        {
            canProjectile.transform.position = canOnPan.transform.position;
            canProjectile.transform.rotation = canOnPan.transform.rotation;
            canProjectile.SetActive(true);
        }

        canProjectileController.Fly(target.position);
    }
}
