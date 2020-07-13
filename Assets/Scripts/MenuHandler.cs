using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the visibility of the Menu
/// </summary>
public class MenuHandler : MonoBehaviour
{

    /// <summary>
    /// Instance of itself to get a singleton pattern
    /// </summary>
    public static MenuHandler instance;

    [Tooltip("UI panel corresponding to the menu")]
    public GameObject menuPanel;
    [Tooltip("X position of the menu panel when close")]
    public float xClose;
    [Tooltip("X position of the menu panel when open")]
    public float xOpen;
    [Tooltip("Time to get from open to close position and vice versa")]
    public float tweenTime;

    /// <summary>
    /// Is the menu currently open
    /// </summary>
    private bool isOpen = false;

    /// <summary>
    /// Singleton pattern :
    /// Instanciate a new AudioManager if no existing instance, 
    /// otherwise take the already existing instance.
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Toogle the menu
    /// </summary>
    public void ToogleIsOpen()
    {
        isOpen = !isOpen;
        float to = xOpen;
        if (!isOpen)
            to = xClose;
        LeanTween.moveLocalX(menuPanel, to, tweenTime);
        AudioManager audioManager = AudioManager.instance;
        if (isOpen)
        {
            audioManager.PlaySFX("open_menu");
        }
        else
        {
            audioManager.PlaySFX("close_menu");
        }
    }

    public void CloseMenu()
    {
        isOpen = false;
        LeanTween.moveLocalX(menuPanel, xClose, tweenTime);
        AudioManager.instance.PlaySFX("close_menu");
    }
}
