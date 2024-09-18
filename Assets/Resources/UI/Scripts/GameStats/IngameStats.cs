using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameStats : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeEnum stat; // El tipo de estad�stica que se mostrar�.

    TextMeshProUGUI statText; // El componente de texto de TextMeshProUGUI que muestra la estad�stica.

    SOPlayerInfo playerInfo; // La informaci�n del jugador que contiene las estad�sticas.

    private void OnEnable()
    {
        // Obtiene la informaci�n del jugador y el componente de texto.
        playerInfo = PlayerStats.instance.soPlayerInfo;
        statText = GetComponentInChildren<TextMeshProUGUI>();

        // Capitaliza el nombre de la estad�stica y actualiza el texto con el valor actual.
        string name = StringUtils.CapitalizeFirstLetter(stat.ToString());
        statText.text = name + " : " + playerInfo.statUpgrades[stat].ToString();
    }
}
