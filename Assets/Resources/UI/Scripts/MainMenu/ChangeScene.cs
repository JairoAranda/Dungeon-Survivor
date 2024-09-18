using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] bool mainMenu; // Indica si el botón de cambio de escena está en el menú principal.

    // Cambia la escena a la especificada por el índice de escena.
    public void ChangeSceneButton (int scene)
    {
        if (mainMenu)
        {
            if (SelectedCharacterManager.instance.player != null)
            {
                LoadingManager.instance.sceneIndex = scene;
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            LoadingManager.instance.sceneIndex = scene;
            SceneManager.LoadScene(2);
        }
        
    }

    // Cambia el valor de escala del tiempo del juego.
    public void ChangeTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }

    // Destruye el objeto del jugador actual.
    public void DestoyPlayer()
    {
        Destroy(PlayerStats.instance.gameObject);
    }
}
