using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusController : MonoBehaviour
{
    public static CameraFocusController Instance;

    [SerializeField]
    private List<Transform> focusObjects;

    public float widthScalar, lengthScalar, minDistance, moveSpeed;

    private float distance;

    private Vector3 viewDir;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        viewDir = -transform.forward;

        if (focusObjects == null)
        {
            focusObjects = new List<Transform>();
        }
    }

    void Update()
    {
        transform.position =  Vector3.MoveTowards(transform.position, boundingBoxCenter(), moveSpeed*Time.deltaTime);
    }



    private Vector3 boundingBoxCenter()
    {
        float xMin = focusObjects.Min(x => x.position.x);
        float xMax = focusObjects.Max(x => x.position.x);
        float zMin = focusObjects.Min(x => x.position.z);
        float zMax = focusObjects.Max(x => x.position.z);

        Vector3 center = new Vector3((xMin + xMax) / 2, 0, (zMin + zMax) / 2);
        float xDist = (xMax - xMin) * widthScalar/2;
        float yDist = (zMax - zMin) * lengthScalar/2;
        float radialDist = Mathf.Sqrt(xDist * xDist + yDist * yDist);

        //Taking the mean sqare avg of the mindistance and box distance
        distance = Mathf.Sqrt(radialDist*radialDist + minDistance*minDistance);
        return center + distance * viewDir;
    }
}
