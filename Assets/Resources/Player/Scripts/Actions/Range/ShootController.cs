using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ShootController : EnemyDetector
{
    [SerializeField] bool isAuto;

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

        float lifeTime = detectionRange / (sOFinderPlayer.projectileSpeed / 100);

        if (closestEnemy != null && lastShootTime > shootCooldown)
        {
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, sOFinderPlayer.projectileSpeed, sOFinderPlayer.damage, closestEnemy.position, enemyTag, true, lifeTime);
            lastShootTime = 0;
        }
    }

    void TargetShot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0;

        Debug.Log(mousePosition);

        float lifeTime = detectionRange / (sOFinderPlayer.projectileSpeed / 50);

        if (lastShootTime > shootCooldown)
        {
            ProjectilePool.instance.ShootBullet(gameObject.transform.position, sOFinderPlayer.projectileSpeed, sOFinderPlayer.damage, mousePosition, enemyTag, true, lifeTime);
            lastShootTime = 0;
        }
    }

}
