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
        StartCoroutine(CheckMenu());
    }

    IEnumerator CheckMenu()
    {
        while (xpMenu.activeSelf)
        {
            yield return new WaitForFixedUpdate();
        }

        chestMenu.SetActive(true);
    }
}
