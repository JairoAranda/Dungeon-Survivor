using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenus : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0; // Detiene el tiempo cuando el menú de pausa se habilita.
    }

    private void OnDisable()
    {
        Time.timeScale = 1; // Reanuda el tiempo cuando el menú de pausa se deshabilita.
    }
}
