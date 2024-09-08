using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void ChangeSceneButton (int scene)
    {
        if (SelectedCharacterManager.instance.player != null)
        {
            SceneManager.LoadScene(scene);
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
