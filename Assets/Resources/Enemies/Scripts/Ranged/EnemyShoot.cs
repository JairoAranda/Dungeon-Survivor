using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceToPlayer))]
[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Color bulletColor;

    private IBulletType effect;

    private DistanceToPlayer distanceToPlayer;
    private SOEnemyInfo enemyInfoSO;

    private float shootInterval;
    private float shootTimer;
    void Start()
    {
        distanceToPlayer = GetComponent<DistanceToPlayer>();
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;

        shootInterval = enemyInfoSO.attackSpeed;

        effect = GetComponent<IBulletType>();

    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (distanceToPlayer.NearPlayer() && shootTimer >= shootInterval)
        {
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, enemyInfoSO.projectileSpeed, enemyInfoSO.damage, enemyInfoSO.attackRange + 40 ,PlayerStats.instance.transform.position, transform.position ,playerLayer, gameObject, bulletColor);
            shootTimer = 0;
        }
    }
}