using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    //Game manager and sound
    public GameManager gameManager;
    public AudioSource buttonClick;

    //Switches to options menu
    public void OptionsSwitch()
    {
        if(gameManager != null)
        {
            buttonClick.Play();
            gameManager.ActivateOptionsScreen();
        }
    }
}
