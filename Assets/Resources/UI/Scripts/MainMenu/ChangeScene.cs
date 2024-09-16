using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] bool mainMenu;

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


    public void ChangeTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void DestoyPlayer()
    {
        Destroy(PlayerStats.instance.gameObject);
    }
}
