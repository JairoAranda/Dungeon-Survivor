using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeManager : MonoBehaviour
{
    public static event Action<GameObject> EventTriggerAccept, EventTriggerDeny; // Evento que se activa cuando la actualización es aceptada o denegada.

    int upgradeMoney; // Cantidad de dinero necesaria para la actualización

    int maxLevels; // Número máximo de niveles posibles

    GameObject[] levels; // Arreglo de niveles

    [SerializeField] PlayerPrefsEnum currentMoneyPrefs; // El tipo de preferencia que se usa para el dinero actual del jugador.

    // Configura el arreglo de niveles basándose en el objeto de nivel proporcionado.
    public void LevelArray(GameObject levelGO)
    {
        Transform[] childTransforms = levelGO.GetComponentsInChildren<Transform>();
        levels = new GameObject[childTransforms.Length - 1];

        for (int i = 1; i < childTransforms.Length; i++)
        {
            levels[i - 1] = childTransforms[i].gameObject;
        }

        maxLevels = levels.Length;
    }

    // Establece la cantidad de dinero necesaria para la actualización.
    public void BaseMoney(int money)
    {
       upgradeMoney = money;
    }

    // Maneja el botón de actualización y verifica si el jugador tiene suficiente dinero para realizar la actualización.
    public void UpgradeButton(GameObject go)
    {
        PlayerPrefsEnum stat = go.GetComponent<CheckPlayerPref>().statPrefs;

        int level = PlayerPrefs.GetInt(stat.ToString(), 1);

        // Calcula el costo de actualización basado en el nivel actual
        upgradeMoney *= level;

        if (MoneyManager.instance.money >= upgradeMoney && level <= maxLevels)
        {
            EventTriggerAccept(gameObject);

            levels[level - 1].GetComponent<Image>().color = Color.yellow;

            level++;

            PlayerPrefs.SetInt(stat.ToString(), level);

            MoneyManager.instance.money -= upgradeMoney;

            PlayerPrefs.SetInt(currentMoneyPrefs.ToString(), MoneyManager.instance.money);

        }

        else
        {
            EventTriggerDeny(gameObject);
        }
 
    }
}
