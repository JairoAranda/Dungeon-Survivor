using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentMenus;

    public void ChangeMenu()
    {
        foreach (GameObject menu in currentMenus)
        {
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        }

    }
}
