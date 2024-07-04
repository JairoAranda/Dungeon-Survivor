using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ShootController : MonoBehaviour
{
    [SerializeField] string enemyTag = "Enemy";

    private SOPlayerInfo sOFinderPlayer;

    private float detectionRange;
    private float shootCooldown;

    private float lastShootTime;

    void Start()
    {
        sOFinderPlayer = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        detectionRange = sOFinderPlayer.range;
        shootCooldown = sOFinderPlayer.cooldown;
    }

    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        Transform closestEnemy = null;
        float closestDistance = detectionRange;

        lastShootTime += Time.deltaTime;

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

        if (closestEnemy != null && lastShootTime > shootCooldown)
        {
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, sOFinderPlayer.projectileSpeed, sOFinderPlayer.damage, closestEnemy, enemyTag);
            lastShootTime = 0;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
