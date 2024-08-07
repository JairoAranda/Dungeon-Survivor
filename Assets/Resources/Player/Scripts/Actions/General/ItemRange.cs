using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ItemRangeAbsortion : MonoBehaviour
{
    [SerializeField] float detectionRadiusDefault = 20f;
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
        absortion = GetComponent<SOFinderPlayer>().sOPlayerInfo.absortion;

        scaleFactor = 1f + ((float)(absortion - 1) / 19f) * 4f;

        Debug.Log(scaleFactor);

        detectionRadius = detectionRadiusDefault * scaleFactor;
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
