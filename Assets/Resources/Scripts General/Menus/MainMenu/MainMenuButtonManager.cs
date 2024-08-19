using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    public void ChangeScene (int scene)
    {
        SceneManager.LoadScene (scene);
    }

    public void ChangeMenu(GameObject nextMenu)
    {
        GameObject currentMenu = gameObject.GetComponentInParent<GameObject> ();

        nextMenu.SetActive (true);

        currentMenu.SetActive (false);
    }
}
