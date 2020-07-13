using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represent a spawnable cube
/// </summary>
public class Cube : MonoBehaviour
{
    [Tooltip("ScriptableObject with all data needed")]
    public CubeProfile profile;

    // Start is called before the first frame update
    void Start()
    {
        SetupCube();
    }

    /// <summary>
    /// Setup the cube with the data from the profile.
    /// </summary>
    private void SetupCube()
    {
        GetComponent<Renderer>().sharedMaterial.color = profile.color;
    }

    /// <summary>
    /// Set a new profile for this cube
    /// </summary>
    /// <param name="profile"></param>
    public void SetProfile(CubeProfile profile)
    {
        this.profile = profile;
        SetupCube();
    }
}
