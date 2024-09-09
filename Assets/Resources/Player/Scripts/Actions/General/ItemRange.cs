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
    [SerializeField] int multiplier = 5;

    [Header("Object Layer")]
    [Space]
    [SerializeField] LayerMask detectionLayer;

    [Header("Stat Type")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum absortionUpgrade;

    [SerializeField] private PlayerPrefsEnum absortionPrefs;

    float detectionRadiusDefault;

    private int absortion;
    private float detectionRadius;

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
        detectionRadiusDefault = GetComponent<SOFinderPlayer>().sOPlayerInfo.absortion;


        UpdateDetectionRange();
    }

    void UpdateDetectionRange()
    {
        absortion = GetComponent<SOFinderPlayer>().sOPlayerInfo.statUpgrades[absortionUpgrade];

        detectionRadius = detectionRadiusDefault * PlayerPrefs.GetInt(absortionPrefs.ToString(), 1) * ScaleMultiplier.ScaleFactor(multiplier, absortion);
    }

    void Update()
    {
        DetectItems();
    }

    void DetectItems()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);

        foreach (var hitCollider in hitColliders)
        {
            GeneralReciveDrop[] generalReciveDrops = hitCollider.GetComponents<GeneralReciveDrop>();

            foreach (var drop in generalReciveDrops)
            {
                if (drop.enabled && !drop.isPlaying)
                {
                    drop.isPlaying = true;
                    drop.StartAnim();
                }
            }

        }
    }
}
