using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropAmount : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private int minDrop = 0, maxDrop = 4;
    
    private static System.Random random = new System.Random();

    private int luck;

    void Start()
    {
        luck = PlayerStats.instance.GetComponentInParent<SOFinderPlayer>().sOPlayerInfo.luck;
       
    }

    public int GetDropNumber()
    {
        int range = maxDrop - minDrop + 1;

        double luckFactor = Math.Pow(2, luck / 10.0) - 1;
        double step = 1.0 / range;
        double[] thresholds = new double[range];

        for (int i = 0; i < range; i++)
        {
            thresholds[i] = step * (i + 1) - luckFactor / range;
        }

        // Generate a random number between 0 and 1
        double randomValue = random.NextDouble();

        // Determine the random number based on the thresholds
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
