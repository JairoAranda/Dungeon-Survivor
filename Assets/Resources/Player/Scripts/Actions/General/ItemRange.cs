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
    [SerializeField] int multiplier = 5; // Multiplicador para la absorción de rango

    [Header("Object Layer")]
    [Space]
    [SerializeField] LayerMask detectionLayer; // Capa para detectar los objetos que se pueden absorber

    [Header("Stat Type")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum absortionUpgrade; // Enum que indica el tipo de mejora de absorción

    [SerializeField] private PlayerPrefsEnum absortionPrefs; // Enum para las preferencias guardadas de absorción

    float detectionRadiusDefault; // Radio de detección por defecto

    private int absortion; // Valor de absorción basado en las mejoras
    private float detectionRadius; // Radio de detección ajustado

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
        // Obtiene el valor de absorción por defecto del SOFinderPlayer
        detectionRadiusDefault = GetComponent<SOFinderPlayer>().sOPlayerInfo.absortion;

        UpdateDetectionRange();
    }

    void UpdateDetectionRange()
    {
        // Obtiene el valor de absorción basado en las mejoras del jugador
        absortion = GetComponent<SOFinderPlayer>().sOPlayerInfo.statUpgrades[absortionUpgrade];

        // Calcula el radio de detección ajustado con base en el valor de absorción y las preferencias guardadas
        detectionRadius = detectionRadiusDefault * PlayerPrefs.GetInt(absortionPrefs.ToString(), 1) * ScaleMultiplier.ScaleFactor(multiplier, absortion);
    }

    void Update()
    {
        DetectItems();
    }

    void DetectItems()
    {
        // Obtiene todos los colliders dentro del radio de detección
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);

        foreach (var hitCollider in hitColliders)
        {
            // Obtiene todos los componentes GeneralReciveDrop en el collider
            GeneralReciveDrop[] generalReciveDrops = hitCollider.GetComponents<GeneralReciveDrop>();

            foreach (var drop in generalReciveDrops)
            {
                // Si el componente está habilitado y no está activa la animación
                if (drop.enabled && !drop.isPlaying)
                {
                    drop.isPlaying = true; // Marca el drop como en animación
                    drop.StartAnim(); // Inicia la animación
                }
            }

        }
    }
}
