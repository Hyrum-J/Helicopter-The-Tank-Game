using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class ExitButton : MonoBehaviour
{
    public AudioSource buttonClick;
    public void ExitGame()
    {
        buttonClick.Play();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
