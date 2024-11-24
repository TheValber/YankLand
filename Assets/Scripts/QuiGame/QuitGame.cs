using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quits the game when the escape key is pressed.
/// </summary>
public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
