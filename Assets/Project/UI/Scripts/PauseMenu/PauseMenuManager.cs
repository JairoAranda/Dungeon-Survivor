using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // El menú de pausa que se activa o desactiva.

    [Header("Inputs")]
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] InputActionEnum pauseInputAction;
    private InputAction pauseAction;

    private void OnEnable()
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay"); // Encuentra el mapa de acción "Gameplay"
        pauseAction = gameplayMap.FindAction(pauseInputAction.ToString());
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    private void Update()
    {
        if (pauseAction.triggered)
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
