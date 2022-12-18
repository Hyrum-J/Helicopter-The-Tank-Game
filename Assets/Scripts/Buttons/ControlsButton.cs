using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    //Game manager and sound for when clicked
    public GameManager gameManager;
    public AudioSource buttonClick;

    //Switches to the controls menu
    public void ControlSwitch()
    {
        if (gameManager != null)
        {
            buttonClick.Play();
            gameManager.ActivateControlsScreen();
        }
    }
}
