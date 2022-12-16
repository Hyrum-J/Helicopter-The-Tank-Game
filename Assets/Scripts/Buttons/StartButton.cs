using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameManager gameManager;
    public void GameStarter()
    {
        if(gameManager != null)
        {
            gameManager.ActivateMainGameScreen();
        }
    }
}
