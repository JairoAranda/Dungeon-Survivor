using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ShootController : EnemyDetector
{
    [Header("Bullet Color")]
    [Space]
    [SerializeField] Color bulletColor;


    void Update()
    {
        lastShootTime += Time.deltaTime;

        if (OptionManager.instance.isAuto)
        {
            AutoShot();
        }
        else if (Input.GetMouseButton(0))
        {
            TargetShot();
        }
    }

    void AutoShot()
    {
        Transform closestEnemy = DetectClosestEnemy();

        float lifeTime = detectionRange / (sOPlayerInfo.projectileSpeed / 50);

        if (closestEnemy != null && lastShootTime > shootCooldown)
        {
            ProjectilePool.instance.ShootBullet(handPosition.position, sOPlayerInfo.projectileSpeed, PlayerStats.instance.dmg, detectionRange, closestEnemy.position, handPosition.position ,detectionLayer, gameObject, bulletColor);
            lastShootTime = 0;
        }
    }

    void TargetShot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0;

        float lifeTime = detectionRange / (sOPlayerInfo.projectileSpeed / 50);

        if (lastShootTime > shootCooldown)
        {
            ProjectilePool.instance.ShootBullet(handPosition.position, sOPlayerInfo.projectileSpeed, PlayerStats.instance.dmg, detectionRange, mousePosition, handPosition.position ,detectionLayer, gameObject, bulletColor);
            lastShootTime = 0;
        }
    }

}
