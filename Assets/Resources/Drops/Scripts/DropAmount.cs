using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAmount : MonoBehaviour
{    
    private int luck;

    [SerializeField] private PlayerUpgradeEnum luckUpgrade;

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
        GetLuck();
    }

    void GetLuck()
    {
        luck = PlayerStats.instance.GetComponentInParent<SOFinderPlayer>().sOPlayerInfo.statUpgrades[luckUpgrade];
    }

    public int GetDropNumber(int minDrop, int maxDrop, double minProbabilityMaxDrop, double maxProbabilityMaxDrop)
    {
        float multiplier = (float)(1 + 0.1 * PlayerPrefs.GetInt("Luck", 1) - 0.1);

        luck = Mathf.Clamp(luck, 1, 20);

        // Interpolar la probabilidad de obtener maxDrop entre minprobabilityMaxDrop (luck = 1) y probabilityMaxDrop (luck = 20)
        double interpolatedProbability = Mathf.Lerp((float)minProbabilityMaxDrop, (float)maxProbabilityMaxDrop, (float)(luck - 1) / 19f);

        double currentprobabilityMaxDrop = interpolatedProbability * multiplier;

        currentprobabilityMaxDrop = Mathf.Clamp((float)currentprobabilityMaxDrop, 0.01f, 1.0f);

        int range = maxDrop - minDrop;

        // Calcular el factor de suerte basado en la probabilidad interpolada
        float luckFactor = Mathf.Clamp01((float)currentprobabilityMaxDrop);

        float randomValue = Random.Range(0f, 1f);

        int result;
        // Si el valor aleatorio es menor o igual al factor de suerte, dar el valor máximo
        if (randomValue <= luckFactor)
        {
            result = maxDrop;
        }
        else
        {
            // De lo contrario, calcular un valor basado en el rango
            result = Mathf.FloorToInt(minDrop + randomValue * range);
        }

        // Restringir el resultado dentro del rango válido
        result = Mathf.Clamp(result, minDrop, maxDrop);

        return result;
    }
}
