using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LavaCanController : MonoBehaviour
{
    [SerializeField]
    private float timeToTarget = default;

    [SerializeField]
    private AnimationCurve yCurve = default;

    [SerializeField]
    private GameObject explosionPrefab = default;

    private GameObject instantiatedExplosion;
    private LavaExplosionController instantiatedExplosionController;

    private static readonly Quaternion angularRotation = Quaternion.Euler(-90f, 0, 0);

    void OnDestroy()
    {
        if (instantiatedExplosion)
            Destroy(instantiatedExplosion);
    }

    public void Fly(Vector3 target)
    {
        StartCoroutine(MoveToPosition(target, timeToTarget));
    }

    private IEnumerator MoveToPosition(Vector3 input, float time)
    {
        float startTime = Time.time;
        Vector3 startPos = transform.position;
        while (startTime + time > Time.time)
        {
            float x = (Time.time - startTime) / time;
            transform.position = startPos * (1 - x) + input * x + yCurve.Evaluate(x) * Vector3.up;
            transform.rotation *= Quaternion.Lerp(Quaternion.identity, angularRotation, Time.deltaTime * 10f);

            yield return new WaitForEndOfFrame();
        }

        if (instantiatedExplosion)
        {
            instantiatedExplosion.transform.position = input;
            instantiatedExplosion.SetActive(true);
        } else
        {
            instantiatedExplosion = Instantiate(explosionPrefab, input, Quaternion.identity);
            instantiatedExplosionController = instantiatedExplosion.GetComponent<LavaExplosionController>();
        }

        instantiatedExplosionController.Explode();
        gameObject.SetActive(false);
    }
}
