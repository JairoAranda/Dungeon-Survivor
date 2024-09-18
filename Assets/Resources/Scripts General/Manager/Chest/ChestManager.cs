using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static ChestManager instance;

    [SerializeField] GameObject chestMenu; // Men� del cofre.
    [SerializeField] GameObject xpMenu; // Men� de experiencia.

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

    // Activa el men� del cofre, asegur�ndose de que el men� de experiencia no est� activo.
    public void ActiveChestMenu()
    {
        StartCoroutine(CheckMenu());
    }

    // Verifica si el men� de experiencia est� activo y espera hasta que se desactive antes de activar el men� del cofre.
    IEnumerator CheckMenu()
    {
        while (xpMenu.activeSelf)
        {
            yield return new WaitForFixedUpdate();
        }

        chestMenu.SetActive(true);
    }
}
