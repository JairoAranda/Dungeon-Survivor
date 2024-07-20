using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ShootController : EnemyDetector
{
    public bool isAuto;

    [SerializeField] Transform handPosition;

    void Update()
    {
        lastShootTime += Time.deltaTime;

        if (isAuto)
        {
            AutoShot();
        }
        else if (Input.GetMouseButtonDown(0))
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
            ProjectilePool.instance.ShootBullet(handPosition.position, sOPlayerInfo.projectileSpeed, sOPlayerInfo.damage, closestEnemy.position, detectionLayer ,true, lifeTime);
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
            ProjectilePool.instance.ShootBullet(handPosition.position, sOPlayerInfo.projectileSpeed, sOPlayerInfo.damage, mousePosition, detectionLayer, true, lifeTime);
            lastShootTime = 0;
        }
    }

}
