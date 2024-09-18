using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentMenus; // Men�s actualmente visibles que se deben desactivar

    // Cambia el men� actual al siguiente men� especificado.
    public void ChangeMenu(GameObject nextMenu)
    {
        // Desactiva todos los men�s actualmente visibles
        foreach (GameObject menu in currentMenus)
        {
            menu.SetActive(false);
        }

        // Activa el nuevo men�
        nextMenu.SetActive(true);
    }
}
