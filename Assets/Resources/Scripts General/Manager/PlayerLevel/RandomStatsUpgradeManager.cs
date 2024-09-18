using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomStatsUpgradeManager : MonoBehaviour
{
    /// Evento que se dispara cuando se actualiza una estad�stica.
    public static event Action EventTriggerOnUpgradeStat; 

    [SerializeField] Button[] upgradeCards; // Botones para las opciones de mejora.

    private PlayerUpgradeEnum?[] keyButtons; // Almacena las claves de estad�sticas asignadas a cada bot�n.

    private HashSet<PlayerUpgradeEnum> assignedValues; // Conjunto de estad�sticas asignadas para evitar duplicados.

    Dictionary<PlayerUpgradeEnum, int> dictionary; // Diccionario de estad�sticas del jugador.


    private void OnEnable()
    {
        // Inicializa el diccionario de estad�sticas del jugador.
        dictionary = PlayerStats.instance.soPlayerInfo.statUpgrades;

        // Inicializa las claves de los botones y el conjunto de valores asignados.
        keyButtons = new PlayerUpgradeEnum?[upgradeCards.Length];
        assignedValues = new HashSet<PlayerUpgradeEnum>();

        // Limpia los listeners de clic en los botones.
        foreach (var upgradeCard in upgradeCards)
        {
            upgradeCard.onClick.RemoveAllListeners();
        }

        // Asigna una estad�stica aleatoria a cada bot�n y a�ade listeners para los clics.
        for (int i = 0; i < upgradeCards.Length; i++)
        {
            int index = i;

            AssingRandomStat(upgradeCards[index], out keyButtons[index]);

            if (keyButtons[index].HasValue)
            {
                upgradeCards[index].onClick.AddListener(() => IncrementValue(keyButtons[index].Value));
            }
            else
            {
                upgradeCards[index].onClick.AddListener(() => GetMoney());
            }
        }
    }

    // Asigna una estad�stica aleatoria a un bot�n.
    void AssingRandomStat(Button button, out PlayerUpgradeEnum? key)
    {
        // Obtiene las claves disponibles que no han sido asignadas y cuyo valor es menor de 20.
        var availableKeys = dictionary.Where(kv => kv.Value < 20 && !assignedValues.Contains(kv.Key))
                                    .Select(kv => kv.Key)
                                    .ToList();

        // Si no hay claves disponibles, asigna "Dinero" al bot�n.
        if (availableKeys.Count == 0)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Dinero";
            key = null;
            return;
        }

        // Selecciona una clave aleatoria.
        int randomIndex = UnityEngine.Random.Range(0, availableKeys.Count);

        key = availableKeys[randomIndex];

        assignedValues.Add(key.Value);

        // Establece el texto del bot�n con el nombre de la estad�stica.
        string name = StringUtils.CapitalizeFirstLetter(key.ToString());
        button.GetComponentInChildren<TextMeshProUGUI>().text = name;
    }

    // Incrementa el valor de una estad�stica en el diccionario.
    void IncrementValue(PlayerUpgradeEnum key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key]++;
        }

        // Dispara el evento de actualizaci�n de estad�stica.
        EventTriggerOnUpgradeStat?.Invoke();

        // Desactiva el objeto.
        gameObject.SetActive(false);
    }

    // Otorga una cantidad fija de dinero al jugador.
    void GetMoney()
    {
        MoneyManager.instance.money += 30;


        // Desactiva el objeto.
        gameObject.SetActive(false);
    }

}
