using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceToPlayer))]
[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyShoot : MonoBehaviour
{
    private DistanceToPlayer distanceToPlayer;
    private EnemyInfoSO enemyInfoSO;

    private float shootInterval;
    private float shootTimer;
    void Start()
    {
        distanceToPlayer = GetComponent<DistanceToPlayer>();
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;

        shootInterval = enemyInfoSO.cooldown;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (distanceToPlayer.NearPlayer() && shootTimer >= shootInterval)
        {
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, enemyInfoSO.projectileSpeed, enemyInfoSO.damage);
            shootTimer = 0;
        }
    }
}
