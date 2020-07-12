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

    /// <summary>
    /// Animator component with the menu animations
    /// </summary>
    public Animator animator;

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
        animator.SetBool("is_open", isOpen);
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
        animator.SetBool("is_open", isOpen);
        AudioManager.instance.PlaySFX("close_menu");
    }
}
