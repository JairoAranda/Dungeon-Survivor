using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrop", menuName = "Drops/Drop")]
public class SODrop : ScriptableObject
{
    //[Header("Drop type")]
    //public EnumDropType types;

    [Header("Drop Pool Amount")]
    [Space]
    [Range(1f, 100f)]
    public int dropAmount;

    [Header("Amount Drop Range")]
    [Space]
    [Range(0f, 10f)]
    public int minDrop;
    [Range(0, 10f)]
    public int maxDrop;

    [Header("Max Drop Probability")]
    [Space]
    [Range(0f, 1f)]
    public double minProbabilityMaxDrop;
    [Range(0f, 1f)]
    public double maxProbabilityMaxDrop;

    void OnValidate()
    {
        if (maxDrop < minDrop)
        {
            maxDrop = minDrop;
        }

        if (maxProbabilityMaxDrop < minProbabilityMaxDrop)
        {
            maxProbabilityMaxDrop = minProbabilityMaxDrop;
        }

    }
}
