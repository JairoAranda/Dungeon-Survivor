using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrop", menuName = "Drops/Drop")]
public class SODrop : ScriptableObject
{
    //[Header("Drop type")]
    //public EnumDropType types;

    [Header("Drop Pool Amount")]
    [Range(1f, 20f)]
    public int dropAmount;

    [Header("Amount Drop Range")]
    [Range(0f, 10f)]
    public int minDrop;
    [Range(0, 10f)]
    public int maxDrop;

    [Header("Max Drop Probability")]
    [Range(0f, 1f)]
    public double probabilityMaxDrop;

    void OnValidate()
    {
        if (maxDrop < minDrop)
        {
            maxDrop = minDrop;
        }


    }
}
