using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static ChestManager instance;

    [SerializeField] GameObject chestMenu; // Menú del cofre.
    [SerializeField] GameObject xpMenu; // Menú de experiencia.

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

    // Activa el menú del cofre, asegurándose de que el menú de experiencia no esté activo.
    public void ActiveChestMenu()
    {
        StartCoroutine(CheckMenu());
    }

    // Verifica si el menú de experiencia está activo y espera hasta que se desactive antes de activar el menú del cofre.
    IEnumerator CheckMenu()
    {
        while (xpMenu.activeSelf)
        {
            yield return new WaitForFixedUpdate();
        }

        chestMenu.SetActive(true);
    }
}
