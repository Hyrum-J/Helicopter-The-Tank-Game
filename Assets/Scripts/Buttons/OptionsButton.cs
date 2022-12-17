using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource buttonClick;

    public void OptionsSwitch()
    {
        if(gameManager != null)
        {
            buttonClick.Play();
            gameManager.ActivateOptionsScreen();
        }
    }
}
