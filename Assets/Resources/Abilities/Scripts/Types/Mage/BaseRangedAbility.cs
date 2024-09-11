using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BaseRangedAbility : BaseAbility
{
    public static event Action<GameObject> EventTriggerAbilityShoot;

    private GameObject _bulletToShoot;

    private Transform handPoint;
    private Transform armPosition;

    Rigidbody2D rb;
    public GameObject bulletToShoot
    {
        get => _bulletToShoot;
        set => _bulletToShoot = value;
    }

    [Header("Bullet Color")]
    [Space]
    [ColorUsage(true, true)]
    [SerializeField] Color bulletColor;

    protected override void OnEnable()
    {
        base.OnEnable();

        handPoint = GameObject.FindGameObjectWithTag("Hand").transform;

        armPosition = GameObject.FindGameObjectWithTag("Arm").transform;

    }

    protected virtual void Bullet()
    {
        bulletToShoot = projectilePool.typesInstances[projectilePool.shootNumber];
    }

    protected virtual void AddPool()
    {
        projectilePool.shootNumber++;

        if (projectilePool.shootNumber > projectilePool.poolSize - 1)
        {
            projectilePool.shootNumber = 0;
        }
    }

    protected virtual void SetAtributes()
    {
        Projectile projectile = bulletToShoot.GetComponent<Projectile>();

        projectile.hitLayer = enemyLayer;

        projectile.dmg = PlayerStats.instance.dmg;
        projectile.range = sOPlayerInfo.range;
        projectile.owner = PlayerStats.instance.gameObject;
        projectile.GetComponent<SpriteRenderer>().material.color = bulletColor;
    }

    protected virtual void AddForce(float zAngle)
    {
        bulletToShoot.transform.position = handPoint.position;

        bulletToShoot.SetActive(true);

        float endAngle = zAngle + armPosition.eulerAngles.z;

        bulletToShoot.transform.eulerAngles = new Vector3(0, 0, endAngle);

        rb = bulletToShoot.GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(Mathf.Cos(endAngle * Mathf.Deg2Rad), Mathf.Sin(endAngle * Mathf.Deg2Rad));

        rb.AddForce(direction * sOPlayerInfo.projectileSpeed, ForceMode2D.Impulse);

        EventTriggerAbilityShoot(gameObject);
    }
}
