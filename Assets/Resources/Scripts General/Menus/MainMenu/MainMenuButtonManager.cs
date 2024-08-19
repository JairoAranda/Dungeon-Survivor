using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentMenus;

    public void ChangeScene (int scene)
    {
        SceneManager.LoadScene (scene);
    }

    public void ChangeMenu(GameObject nextMenu)
    {
        foreach (GameObject menu in currentMenus)
        {
            menu.SetActive(false);
        }

        nextMenu.SetActive (true);

    }
}
