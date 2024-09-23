using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrop", menuName = "Drops/Drop")]
public class SODrop : ScriptableObject
{

    [Header("Drop Pool Amount")]
    [Space]
    [Range(1f, 100f)]
    public int dropAmount; // Cantidad total de objetos en el pool de drops

    [Header("Amount Drop Range")]
    [Space]
    [Range(0f, 10f)]
    public int minDrop; // Cantidad m�nima de objetos que pueden caer
    [Range(0, 10f)]
    public int maxDrop; // Cantidad m�xima de objetos que pueden caer

    [Header("Max Drop Probability")]
    [Space]
    [Range(0f, 1f)]
    public double minProbabilityMaxDrop; // Probabilidad m�nima para el m�ximo de objetos a caer
    [Range(0f, 1f)]
    public double maxProbabilityMaxDrop; // Probabilidad m�xima para el m�ximo de objetos a caer

    void OnValidate()
    {
        // Asegura que maxDrop no sea menor que minDrop
        if (maxDrop < minDrop)
        {
            maxDrop = minDrop;
        }

        // Asegura que maxProbabilityMaxDrop no sea menor que minProbabilityMaxDrop
        if (maxProbabilityMaxDrop < minProbabilityMaxDrop)
        {
            maxProbabilityMaxDrop = minProbabilityMaxDrop;
        }

    }
}
