using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenu();
        }
    }
    public void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1;
        }

        else
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0;
        }
    }
}
