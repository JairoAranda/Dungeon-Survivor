using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ItemRangeAbsortion : MonoBehaviour
{
    [SerializeField] float detectionRadiusDefault = 20f;
    [SerializeField] LayerMask detectionLayer;

    private int absortion;

    private void Start()
    {
        absortion = GetComponent<SOFinderPlayer>().sOPlayerInfo.absortion;
    }

    void Update()
    {
        DetectItems();
    }

    void DetectItems()
    {
        float scaleFactor = 1f + (absortion - 1) * (4f / 19f);

        float detectionRadius = detectionRadiusDefault * scaleFactor;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<ReciveDrop>().canRecieve == false)
            {
                hitCollider.GetComponent<ReciveDrop>().canRecieve = true;

            }
        }
    }
}