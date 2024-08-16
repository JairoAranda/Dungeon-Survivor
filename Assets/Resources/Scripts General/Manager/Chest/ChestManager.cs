using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static ChestManager instance;

    [SerializeField] GameObject chestMenu;
    [SerializeField] GameObject xpMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ActiveChestMenu()
    {
        while(xpMenu.activeSelf)
        {
            CheckMenu();
        }

        chestMenu.SetActive(true);
    }

    IEnumerator CheckMenu()
    {
        yield return new WaitForEndOfFrame();

        ActiveChestMenu();
    }
}
