using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Color button, allow to change the color the cubes.
/// </summary>
[RequireComponent(typeof(Button), typeof(Image))]
public class ColorButton : MonoBehaviour
{
    [Tooltip("ScriptableObject with all data needed for a cube. " +
        "Here we need it to set the color of the button and give " +
        "the profile to the cube when the button is selected.")]
    public CubeProfile profile;

    /// <summary>
    /// Image of the button
    /// </summary>
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        image.color = profile.color;
    }

    /// <summary>
    /// Change color of the cubes
    /// then close menu.
    /// </summary>
    public void ChangeCubeColor()
    {
        ARTapToPlaceObject tapToPlaceObject = FindObjectOfType<ARSessionOrigin>()
            .GetComponent<ARTapToPlaceObject>();
        if (!tapToPlaceObject)
        {
            Debug.LogError("Can't find ARTapToPlaceObject component on ARSessionOrigin. " +
                "Either one of them is missing from the scene.");
            return;
        }
        Cube cube = tapToPlaceObject.gameObjectToInstantiate.GetComponent<Cube>();
        if (!cube)
        {
            Debug.LogError("Can't find Cube component on ARTapToPlaceObject.");
            return;
        }
        cube.SetProfile(profile);
        MenuHandler.instance.CloseMenu();
    }
}
