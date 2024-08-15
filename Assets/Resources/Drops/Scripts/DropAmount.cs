using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropAmount : MonoBehaviour
{    
    private static System.Random random = new System.Random();

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

    public int GetDropNumber(int minDrop, int maxDrop, double probabilityMaxDrop)
    {
        int range = maxDrop - minDrop;

        double luckFactor = Math.Pow(2, luck / 10.0) - 1;
        double step = 1.0 / range;
        double[] thresholds = new double[range];

        for (int i = 0; i < range; i++)
        {
            thresholds[i] = Math.Max(step * (i + 1) - luckFactor / range, 0.001);
        }

        if (luck == 20)
        {
            thresholds[range - 1] = 1.0 - probabilityMaxDrop;
        }

        else
        {
            thresholds[range - 1] = Math.Max(1.0 - probabilityMaxDrop - luckFactor / range, 0.001);
        }

        double randomValue = random.NextDouble();

        for(int i = 0; i < range; i++)
        {
            if (randomValue < thresholds[i])
            {
                return minDrop + i;
            }
        }

        return maxDrop;
    }
}
