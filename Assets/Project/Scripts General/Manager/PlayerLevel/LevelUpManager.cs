using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] GameObject upgradeOptionGO; // Objeto de juego que contiene las opciones de mejora.
    [SerializeField] GameObject chestMenu; // Objeto de juego del menú del cofre.

    private void OnEnable()
    {
        PlayerStats.EventTriggerLevelUp += EnableLevelUpOptions;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerLevelUp -= EnableLevelUpOptions;
    }

    // Activa las opciones de mejora cuando el jugador sube de nivel.
    void EnableLevelUpOptions()
    {
        StartCoroutine(CheckMenu());
    }

    // Verifica si el menú del cofre está activo y activa las opciones de mejora cuando no lo está.
    IEnumerator CheckMenu()
    {
        // Espera hasta que el menú del cofre no esté activo.
        while (chestMenu.activeSelf)
        {
            yield return new WaitForFixedUpdate();
        }

        // Activa las opciones de mejora.
        upgradeOptionGO.SetActive(true);
    }
}
