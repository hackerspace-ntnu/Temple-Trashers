using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortOnDamage : MonoBehaviour
{
    public AnimationCurve distortCurve;
    public float distortTime;
    private AudioSource audio_source;

    bool distorting = false;
    void Start()
    {
        BaseController.Singleton.GetComponent<HealthLogic>().onDamage += (d) => distort();
        audio_source = GetComponent<AudioSource>();
    }

    void distort()
    {
        if (!distorting)
        {
            StartCoroutine(DistortRoutine());
        }
    }

    private IEnumerator DistortRoutine()
    {
        distorting = true;
        float startTime = Time.time;
        while (Time.time < startTime + distortTime)
        {
            audio_source.pitch = distortCurve.Evaluate((Time.time - startTime) / distortTime);
            yield return null;
        }
        distorting = false;
    }
}
