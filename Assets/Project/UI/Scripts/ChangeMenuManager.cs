using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentMenus; // Menús actualmente visibles que se deben desactivar

    // Cambia el menú actual al siguiente menú especificado.
    public void ChangeMenu(GameObject nextMenu)
    {
        // Desactiva todos los menús actualmente visibles
        foreach (GameObject menu in currentMenus)
        {
            menu.SetActive(false);
        }

        // Activa el nuevo menú
        nextMenu.SetActive(true);
    }
}
