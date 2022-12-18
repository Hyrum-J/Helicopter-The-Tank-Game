using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //Game manager and sound when clicking
    public GameManager gameManager;
    public AudioSource buttonClick;
    public void BackToMenu()
    {
        if (gameManager != null)
        {
            buttonClick.Play();
            gameManager.ActivateMainMenuScreen();
        }
    }
}
