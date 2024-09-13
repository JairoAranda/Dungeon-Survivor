using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentMenus;

    public void ChangeMenu(GameObject nextMenu)
    {
        foreach (GameObject menu in currentMenus)
        {
            menu.SetActive(false);
        }

        nextMenu.SetActive(true);
    }
}
