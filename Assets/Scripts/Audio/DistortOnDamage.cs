using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistortOnDamage : MonoBehaviour
{
    public AnimationCurve distortCurve;
    public float distortTime;
    bool distorting = false;

    private HealthLogic baseHealthLogic;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        baseHealthLogic = BaseController.Singleton.GetComponent<HealthLogic>();
        baseHealthLogic.onDamage += Distort;
    }

    void OnDestroy()
    {
        baseHealthLogic.onDamage -= Distort;
    }

    private void Distort(DamageInfo d)
    {
        if (!distorting)
            StartCoroutine(DistortRoutine());
    }

    private IEnumerator DistortRoutine()
    {
        distorting = true;

        float startTime = Time.time;
        while (Time.time < startTime + distortTime)
        {
            audioSource.pitch = distortCurve.Evaluate((Time.time - startTime) / distortTime);
            yield return null;
        }

        distorting = false;
    }
}
