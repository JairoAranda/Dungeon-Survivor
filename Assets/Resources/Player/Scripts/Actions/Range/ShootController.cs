using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class ShootController : EnemyDetector
{
    [Header("Bullet Color")]
    [SerializeField] Color bulletColor;

    private IBulletType effect;

    private void OnEnable()
    {
        UpdateEffect.EventTriggerUpdateEffect += Effect;
    }

    private void OnDisable()
    {
        UpdateEffect.EventTriggerUpdateEffect -= Effect;
    }

    protected override void Start()
    {
        base.Start();

        Effect();
    }

    void Effect()
    {
        effect = GetComponentInChildren<IBulletType>();

        lastShootTime = 0;

    }

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
            ProjectilePool.instance.ShootBullet(handPosition.position, sOPlayerInfo.projectileSpeed, PlayerStats.instance.dmg, detectionRange, closestEnemy.position, handPosition.position ,detectionLayer, effect, bulletColor);
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
            ProjectilePool.instance.ShootBullet(handPosition.position, sOPlayerInfo.projectileSpeed, PlayerStats.instance.dmg, detectionRange, mousePosition, handPosition.position ,detectionLayer, effect, bulletColor);
            lastShootTime = 0;
        }
    }

}
