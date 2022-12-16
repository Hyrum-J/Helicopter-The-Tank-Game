using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public GameManager gameManager;

    public void OptionsSwitch()
    {
        if(gameManager != null)
        {
            gameManager.ActivateOptionsScreen();
        }
    }
}
