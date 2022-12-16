using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public void BackToMenu()
    {
        if (gameManager != null)
        {
            gameManager.ActivateMainMenuScreen();
        }
    }
}
