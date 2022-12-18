using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class ExitButton : MonoBehaviour
{
    //Sound for button click
    public AudioSource buttonClick;

    //Exit game
    public void ExitGame()
    {
        buttonClick.Play();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
