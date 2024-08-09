using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("General Config")]
    [SerializeField] protected LayerMask detectionLayer;

    [SerializeField] protected Transform handPosition;

    private protected SOPlayerInfo sOPlayerInfo;

    private protected float shootCooldown;
    private protected float lastShootTime;
    private protected float detectionRange;


    protected virtual void Start()
    {
        sOPlayerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        detectionRange = sOPlayerInfo.range;
        shootCooldown = sOPlayerInfo.cooldown;
    }

    protected Transform DetectClosestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(handPosition.position, detectionRange, detectionLayer);
        Transform closestEnemy = null;
        float closestDistance = detectionRange;

        foreach (var hit in hitColliders)
        {
            float distance = Vector2.Distance(handPosition.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = hit.transform;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
