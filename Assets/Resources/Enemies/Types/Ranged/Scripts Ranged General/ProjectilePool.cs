using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectilePool : GeneralPool
{
    public static ProjectilePool instance;

    private int shootNumber = -1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ShootBullet(Vector2 shootPosition, float m_speed, float m_dmg, Vector3 m_target, string m_tag, bool m_lifeTime, float m_timeToDie)
    {
        shootNumber++;

        if (shootNumber > poolSize - 1)
        {
            shootNumber = 0;
        }

        GameObject bulletToShoot = typesInstances[shootNumber];
        Projectile projectileComponent = bulletToShoot.GetComponent<Projectile>();

        projectileComponent.speed = m_speed;
        projectileComponent.dmg = m_dmg;
        projectileComponent.target = m_target;
        projectileComponent.hitTag = m_tag;
        projectileComponent.lifeTime = m_lifeTime;
        projectileComponent.timeToDie = m_timeToDie;


        bulletToShoot.transform.position = shootPosition;
        bulletToShoot.SetActive(true);
    }

}
