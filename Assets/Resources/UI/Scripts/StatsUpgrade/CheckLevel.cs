using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevel : MonoBehaviour
{
    [Header("Stat config")]
    [Space]
    [SerializeField] GameObject[] levels; // Los objetos de nivel que se actualizan en función del estado del jugador.

    [SerializeField] PlayerPrefsEnum stat; // El tipo de preferencia que se usa para determinar el estado del jugador.
    void Start()
    {
        int i = 2; // Comienza desde el nivel 2

        foreach (GameObject level in levels)
        {
            // Verifica si el valor en PlayerPrefs es mayor o igual al índice actual
            if (PlayerPrefs.GetInt(stat.ToString(), 1) >= i)
            {
                // Cambia el color del nivel a amarillo si la condición es verdadera
                level.GetComponent<Image>().color = Color.yellow;
                i++;
            }
        }
    }

}
