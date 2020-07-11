using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Place an object in the world where the user has touched the screen
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    /// <summary>
    /// Object to instantiate when we place an object
    /// </summary>
    public GameObject gameObjectToInstantiate;

    private ARRaycastManager arRaycastManager;
    /// <summary>
    /// Position of the touch input
    /// </summary>
    private Vector2 touchPosition;

    /// <summary>
    /// List of ARRaycastHit used to get hit results from the ARRaycastManager
    /// </summary>
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    /// <summary>
    /// Check if there is a touch input and if it's the first event thrown for this input.
    /// If yes <paramref name="touchPosition"/> gets its position.
    /// If not, <paramref name="touchPosition"/> is set to its default value (0, 0).
    /// </summary>
    /// <param name="touchPosition">position of the touch input, this object is modified by the function</param>
    /// <returns>True if there is a touch input and it's the first one for this input, false otherwise</returns>
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out touchPosition))
            return;
        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // If touch input and raycast hit a plane, then spawn an object or move the existing spawned object.
            var hitPose = hits[0].pose;
            Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
        }
    }
}
