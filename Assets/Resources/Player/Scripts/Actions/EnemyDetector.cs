using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] protected string enemyTag = "Enemy";

    private protected SOPlayerInfo sOFinderPlayer;

    private protected float shootCooldown;
    private protected float lastShootTime;
    private protected float detectionRange;


    protected void Start()
    {
        sOFinderPlayer = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        detectionRange = sOFinderPlayer.range;
        shootCooldown = sOFinderPlayer.cooldown;
    }

    protected Transform DetectClosestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        Transform closestEnemy = null;
        float closestDistance = detectionRange;

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag(enemyTag))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = hit.transform;
                    closestDistance = distance;
                }
            }
        }

        return closestEnemy;
    }
}
