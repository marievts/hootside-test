using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToMiddleTopScreen : MonoBehaviour
{
    public float topBorder;
    public float distanceZ;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        SetPositionToTopScreen();
    }

    private void SetPositionToTopScreen()
    {
        Vector3 position = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - topBorder, distanceZ));
        gameObject.transform.position = position;
    }
}
