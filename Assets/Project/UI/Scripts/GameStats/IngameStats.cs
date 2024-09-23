using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameStats : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeEnum stat; // El tipo de estadística que se mostrará.

    TextMeshProUGUI statText; // El componente de texto de TextMeshProUGUI que muestra la estadística.

    SOPlayerInfo playerInfo; // La información del jugador que contiene las estadísticas.

    private void OnEnable()
    {
        // Obtiene la información del jugador y el componente de texto.
        playerInfo = PlayerStats.instance.soPlayerInfo;
        statText = GetComponentInChildren<TextMeshProUGUI>();

        // Capitaliza el nombre de la estadística y actualiza el texto con el valor actual.
        string name = StringUtils.CapitalizeFirstLetter(stat.ToString());
        statText.text = name + " : " + playerInfo.statUpgrades[stat].ToString();
    }
}
