using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ItemRangeAbsortion : MonoBehaviour
{
    [Header("Absortion Options")]
    [Range(0.1f, 50f)]
    [SerializeField] float detectionRadiusDefault = 20f;
    [Range(2, 10)]
    [SerializeField] int multiplier = 5;

    [Header("Object Layer")]
    [SerializeField] LayerMask detectionLayer;

    private int absortion;
    private float scaleFactor;
    private float detectionRadius;

    private void Start()
    {
        UpdateDetectionRange();
    }

    public void UpdateDetectionRange()
    {
        absortion = GetComponent<SOFinderPlayer>().sOPlayerInfo.absortionLvl;

        detectionRadius = detectionRadiusDefault * ScaleMultiplier.scaleFactor(multiplier, absortion);
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
