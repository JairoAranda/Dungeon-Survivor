using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ItemRangeAbsortion : MonoBehaviour
{
    [Header("Absortion Multiplier")]
    [Space]
    [Range(2, 10)]
    [SerializeField] int multiplier = 5; // Multiplicador para la absorci�n de rango

    [Header("Object Layer")]
    [Space]
    [SerializeField] LayerMask detectionLayer; // Capa para detectar los objetos que se pueden absorber

    [Header("Stat Type")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum absortionUpgrade; // Enum que indica el tipo de mejora de absorci�n

    [SerializeField] private PlayerPrefsEnum absortionPrefs; // Enum para las preferencias guardadas de absorci�n

    float detectionRadiusDefault; // Radio de detecci�n por defecto

    private int absortion; // Valor de absorci�n basado en las mejoras
    private float detectionRadius; // Radio de detecci�n ajustado

    private void OnEnable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += UpdateDetectionRange;
    }

    private void OnDisable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= UpdateDetectionRange;
    }

    private void Start()
    {
        // Obtiene el valor de absorci�n por defecto del SOFinderPlayer
        detectionRadiusDefault = GetComponent<SOFinderPlayer>().sOPlayerInfo.absortion;

        UpdateDetectionRange();
    }

    void UpdateDetectionRange()
    {
        // Obtiene el valor de absorci�n basado en las mejoras del jugador
        absortion = GetComponent<SOFinderPlayer>().sOPlayerInfo.statUpgrades[absortionUpgrade];

        // Calcula el radio de detecci�n ajustado con base en el valor de absorci�n y las preferencias guardadas
        detectionRadius = detectionRadiusDefault * PlayerPrefs.GetInt(absortionPrefs.ToString(), 1) * ScaleMultiplier.ScaleFactor(multiplier, absortion);
    }

    void Update()
    {
        DetectItems();
    }

    void DetectItems()
    {
        // Obtiene todos los colliders dentro del radio de detecci�n
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);

        foreach (var hitCollider in hitColliders)
        {
            // Obtiene todos los componentes GeneralReciveDrop en el collider
            GeneralReciveDrop[] generalReciveDrops = hitCollider.GetComponents<GeneralReciveDrop>();

            foreach (var drop in generalReciveDrops)
            {
                // Si el componente est� habilitado y no est� activa la animaci�n
                if (drop.enabled && !drop.isPlaying)
                {
                    drop.isPlaying = true; // Marca el drop como en animaci�n
                    drop.StartAnim(); // Inicia la animaci�n
                }
            }

        }
    }
}
