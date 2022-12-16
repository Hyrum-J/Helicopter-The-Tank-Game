using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    public GameManager gameManager;

    public void ControlSwitch()
    {
        if (gameManager != null)
        {
            gameManager.ActivateControlsScreen();
        }
    }
}
