using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAmount : MonoBehaviour
{    
    [SerializeField] private PlayerUpgradeEnum luckUpgrade; // Enum para identificar la estadística de suerte

    [SerializeField] private PlayerPrefsEnum luckPrefs; // Enum para el save de suerte

    private int luck; // Valor de suerte del jugador

    private void OnEnable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += GetLuck;
    }

    private void OnDisable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= GetLuck;
    }

    void Start()
    {
        // Esperar hasta el final del frame para asegurar que todas las referencias estén listas
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        GetLuck(); 
    }

    void GetLuck()
    {
        // Obtener el valor de suerte basado en el `luckUpgrade` del jugador
        luck = PlayerStats.instance.soPlayerInfo.statUpgrades[luckUpgrade];
    }

    public int GetDropNumber(int minDrop, int maxDrop, double minProbabilityMaxDrop, double maxProbabilityMaxDrop)
    {
        // Calcular el multiplicador de suerte basado en las preferencias guardadas del jugador
        float multiplier = (float)(1 + 0.1 * PlayerPrefs.GetInt(luckPrefs.ToString(), 1) - 0.1);

        // Limitar el valor de suerte entre 1 y 20
        luck = Mathf.Clamp(luck, 1, 20);

        // Interpolar la probabilidad de obtener el valor máximo de drop
        double interpolatedProbability = Mathf.Lerp((float)minProbabilityMaxDrop, (float)maxProbabilityMaxDrop, (float)(luck - 1) / 19f);

        // Calcular la probabilidad actual de obtener el valor máximo de drop
        double currentprobabilityMaxDrop = interpolatedProbability * multiplier;
        currentprobabilityMaxDrop = Mathf.Clamp((float)currentprobabilityMaxDrop, 0.01f, 1.0f);

        // Calcular el rango de posibles valores de drop
        int range = maxDrop - minDrop;

        // Determinar el factor de suerte basado en la probabilidad interpolada
        float luckFactor = Mathf.Clamp01((float)currentprobabilityMaxDrop);

        // Generar un valor aleatorio entre 0 y 1
        float randomValue = Random.Range(0f, 1f);

        int result;
        // Si el valor aleatorio es menor o igual al factor de suerte, dar el valor máximo
        if (randomValue <= luckFactor)
        {
            result = maxDrop;
        }
        else
        {
            // De lo contrario, calcular un valor basado en el rango de valores posibles
            result = Mathf.FloorToInt(minDrop + randomValue * range);
        }

        // Asegurarse de que el resultado esté dentro del rango válido
        result = Mathf.Clamp(result, minDrop, maxDrop);

        return result;
    }
}
