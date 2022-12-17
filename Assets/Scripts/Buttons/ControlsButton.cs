using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource buttonClick;

    public void ControlSwitch()
    {
        if (gameManager != null)
        {
            buttonClick.Play();
            gameManager.ActivateControlsScreen();
        }
    }
}
