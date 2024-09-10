using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] bool needPlayer;

    public void ChangeSceneButton (int scene)
    {
        if (needPlayer)
        {
            if (SelectedCharacterManager.instance.player != null)
            {
                SceneManager.LoadScene(scene);
            }
        }
        else
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
