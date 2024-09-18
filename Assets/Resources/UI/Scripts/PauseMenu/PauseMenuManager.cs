using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // El menú de pausa que se activa o desactiva.

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    // Activa o desactiva el menú de pausa y ajusta el estado del tiempo en el juego.
    public void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1; // Reanuda el tiempo
        }

        else
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0; // Detiene el tiempo
        }
    }
}
