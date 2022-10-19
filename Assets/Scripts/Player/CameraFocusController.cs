using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class CameraFocusController : MonoBehaviour
{
    public static CameraFocusController Singleton { get; private set; }

    // Transforms the camera tries to focus on
    [SerializeField]
    private List<Transform> focusObjects = default;

    // Scalars for how far the camera zooms depending on the horizontal and vertical distance between focus objects
    [SerializeField]
    private float widthScalar = default;

    [SerializeField]
    private float lengthScalar = default;

    // Minimum distance the camera is from the `focusObjects`
    [SerializeField]
    private float minDistance = default;

    // Overview distance
    [SerializeField]
    private float overviewDistance = default;

    // Are we in overview mode
    [SerializeField]
    private bool overview = default;

    // The Lerp point; higher = more snappy and responsive
    [SerializeField]
    private float smoothing = default;

    // Variables for calculating the ideal position of the camera
    private float distance;
    private Vector3 viewDir;

    void Awake()
    {
        #region Singleton boilerplate

        if (Singleton != null)
        {
            if (Singleton != this)
            {
                Debug.LogWarning($"There's more than one {Singleton.GetType()} in the scene!");
                Destroy(gameObject);
            }

            return;
        }

        Singleton = this;

        #endregion Singleton boilerplate

        viewDir = -transform.forward;

        if (focusObjects == null)
            focusObjects = new List<Transform>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, BoundingBoxCenter(), smoothing * Time.deltaTime);
    }

    private Vector3 BoundingBoxCenter()
    {
        //Makes a "Bounding box"
        float xMin = focusObjects.Min(x => x.position.x);
        float xMax = focusObjects.Max(x => x.position.x);
        float zMin = focusObjects.Min(x => x.position.z);
        float zMax = focusObjects.Max(x => x.position.z);

        //Calculates the ideal position of the camera base on the bounding box postition and size
        Vector3 center = new Vector3((xMin + xMax) / 2, 0, (zMin + zMax) / 2);
        float xDist = (xMax - xMin) * widthScalar / 2;
        float yDist = (zMax - zMin) * lengthScalar / 2;
        float radialDist = Mathf.Sqrt(xDist * xDist + yDist * yDist);

        //Taking the mean sqare avg of the mindistance and box distance
        if (!overview)
            distance = Mathf.Sqrt(radialDist * radialDist + minDistance * minDistance);
        else
            distance = Mathf.Sqrt(radialDist * radialDist + overviewDistance * overviewDistance);

        return center + distance * viewDir;
    }

    //To add a focus point to the camera, use CameraFocusController.Singleton.AddFocusObject(<your transform>);
    public void AddFocusObject(Transform obj)
    {
        if (!focusObjects.Contains(obj))
            focusObjects.Add(obj);
    }

    public void RemoveFocusObject(Transform obj)
    {
        focusObjects.Remove(obj);
    }

    /// <summary>
    /// Replace all focus objects with a single target
    /// </summary>
    public void Focus(Transform obj)
    {
        focusObjects.Clear();
        AddFocusObject(obj);

        // Switch to overview mode
        EnableOverview();
    }

    public void EnableOverview()
    {
        if (!overview)
        {
            overview = true;
            smoothing /= 10;
        }
    }
}
