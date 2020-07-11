using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the visibility of the Menu
/// </summary>
public class HandleMenu : MonoBehaviour
{
    /// <summary>
    /// Animator component with the menu animations
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Is the menu currently open
    /// </summary>
    private bool isOpen = false;

    /// <summary>
    /// Toogle the menu
    /// </summary>
    public void toogleIsOpen()
    {
        isOpen = !isOpen;
        animator.SetBool("is_open", isOpen);
    }
}
