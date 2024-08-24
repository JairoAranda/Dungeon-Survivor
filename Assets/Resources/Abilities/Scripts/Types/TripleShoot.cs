using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityAsign))]
public class TripleShoot : BaseAbility, IAbility
{
    [Header("Aperture Angle")]
    [Space]
    [Range(.1f, 40f)]
    [SerializeField] float shotAngle;

    [Header("Bullet Color")]
    [Space]
    [SerializeField] Color bulletColor;

    private Transform handPoint;
    private Transform armPosition;

    GameObject bulletToShoot;

    Rigidbody2D rb;


    private void OnEnable()
    {
        handPoint = GameObject.FindGameObjectWithTag("Hand").transform;

        armPosition = GameObject.FindGameObjectWithTag("Arm").transform;
    }

    public void Ability()
    {
        for (float i = -shotAngle; i <= shotAngle; i+=shotAngle) 
        {
            AddPool();

            bulletToShoot = projectilePool.typesInstances[projectilePool.shootNumber];

            SetAtributes();

            AddForce(i);
        }
        
    }

    void AddPool()
    {
        projectilePool.shootNumber++;

        if (projectilePool.shootNumber > projectilePool.poolSize - 1)
        {
            projectilePool.shootNumber = 0;
        }
    }

    void AddForce(float zAngle)
    {
        bulletToShoot.transform.position = handPoint.position;

        bulletToShoot.SetActive(true);

        float endAngle = zAngle + armPosition.eulerAngles.z;

        bulletToShoot.transform.eulerAngles = new Vector3(0, 0, endAngle);

        rb = bulletToShoot.GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(Mathf.Cos(endAngle * Mathf.Deg2Rad), Mathf.Sin(endAngle * Mathf.Deg2Rad));

        rb.AddForce(direction * sOPlayerInfo.projectileSpeed, ForceMode2D.Impulse);
    }

    void SetAtributes()
    {
        Projectile projectile = bulletToShoot.GetComponent<Projectile>();

        projectile.hitLayer = enemyLayer;

        projectile.dmg = PlayerStats.instance.dmg;
        projectile.range = sOPlayerInfo.range;
        projectile.owner = PlayerStats.instance.gameObject;
        projectile.GetComponent<SpriteRenderer>().color = bulletColor;
    }
}
