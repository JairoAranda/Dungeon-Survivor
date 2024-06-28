using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyShoot : MonoBehaviour
{
    private EnemyMovement m_EnemyMovement;
    private EnemyInfoSO m_EnemyInfoSO;

    private float shootInterval;
    private float shootTimer;
    void Start()
    {
        m_EnemyMovement = GetComponent<EnemyMovement>();
        m_EnemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;

        shootInterval = m_EnemyInfoSO.cooldown;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (m_EnemyMovement.NearPlayer() && shootTimer >= shootInterval)
        {
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, m_EnemyInfoSO.projectileSpeed);
            shootTimer = 0;
        }
    }
}
