using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceToPlayer))]
[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";

    private DistanceToPlayer distanceToPlayer;
    private SOEnemyInfo enemyInfoSO;

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
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, enemyInfoSO.projectileSpeed, enemyInfoSO.damage, PlayerStats.instance.transform.position, playerTag, false, 0);
            shootTimer = 0;
        }
    }
}
