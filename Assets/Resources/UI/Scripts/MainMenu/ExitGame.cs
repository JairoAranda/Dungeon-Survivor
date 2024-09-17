using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void CloseGame()
    {
                // Si estamos en el editor de Unity
        #if UNITY_EDITOR
                // Detenemos el play mode
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // Si es un build, cerramos la aplicación
                    Application.Quit();
        #endif
    }
}
