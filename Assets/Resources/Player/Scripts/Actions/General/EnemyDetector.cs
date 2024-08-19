using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("General Config")]
    [SerializeField] protected LayerMask detectionLayer;

    [SerializeField] protected Transform handPosition;

    [Header("Lvl Multiplier")]
    [Range(2, 10)]
    [SerializeField] protected int attackSpeedMultiplier;

    [Header("Stat Type")]
    [SerializeField] private PlayerUpgradeEnum attackSpeedUpgrade;

    private protected SOPlayerInfo sOPlayerInfo;

    private protected float shootCooldown;
    private protected float lastShootTime;
    private protected float detectionRange;


    protected virtual void Start()
    {
        sOPlayerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        detectionRange = sOPlayerInfo.range;
        shootCooldown = sOPlayerInfo.attackSpeed * PlayerPrefs.GetInt("AttackSpeed", 1) * ScaleMultiplier.ScaleFactor(attackSpeedMultiplier, sOPlayerInfo.statUpgrades[attackSpeedUpgrade]);
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
