using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AnimationCurve explosionCurve;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(2, 2, 2), 1.5f).setEase(explosionCurve).setOnComplete(() => Destroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
