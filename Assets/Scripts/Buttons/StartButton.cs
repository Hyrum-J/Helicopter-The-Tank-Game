using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    //Game manager and sound for button click
    public GameManager gameManager;
    public AudioSource buttonClick;

    //Starts the game
    public void GameStarter()
    {
        if(gameManager != null)
        {
            buttonClick.Play();
            gameManager.ActivateMainGameScreen();
        }
    }
}
