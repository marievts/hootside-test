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
    [Tooltip("Object to instantiate when we place an object")]
    public GameObject gameObjectToInstantiate;

    private Camera arCamera;
    private ARRaycastManager arRaycastManager;
    /// <summary>
    /// Position of the touch input
    /// </summary>
    private Vector2 touchPosition;
    /// <summary>
    /// Cube currently selected by the user
    /// </summary>
    private Cube selectedObject;

    /// <summary>
    /// List of ARRaycastHit used to get hit results from the ARRaycastManager
    /// </summary>
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arCamera = Camera.main;
    }

    /// <summary>
    /// Check if there is a touch input.
    /// If yes <paramref name="touchPosition"/> gets its position.
    /// If not, <paramref name="touchPosition"/> is set to its default value (0, 0).
    /// </summary>
    /// <param name="touchPosition">position of the touch input, this object is modified by the function</param>
    /// <returns>True if there is a touch input, false otherwise</returns>
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        selectedObject = null;
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out touchPosition))
            return;

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                // If first event of touch, 
                // Did we touch an object or not ?
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    // We touched an object, select it
                    selectedObject = hitObject.transform.GetComponent<Cube>();
                } 
                if (selectedObject == null)
                {
                    // We didn't touch any object, place one
                    GameObject newObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
                    selectedObject = newObject.GetComponent<Cube>();
                    AudioManager.instance.PlaySFX("spawn_object");
                }
            } 
            else if (selectedObject != null)
            {
                // if object selected, then move it
                selectedObject.transform.position = hitPose.position;
                selectedObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
