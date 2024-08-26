using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class ProjectilePool : GeneralPool
{
    public static ProjectilePool instance;

    private Vector3 startPosition, target;

    [HideInInspector]
    public int shootNumber = -1;

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

    public void ShootBullet(Vector2 shootPosition, float m_speed, float m_dmg, float m_range ,Vector3 m_target, Vector3 m_startPosition ,LayerMask m_Layer , GameObject _owner, Color _color)
    {
        shootNumber++;

        if (shootNumber > poolSize - 1)
        {
            shootNumber = 0;
        }

        GameObject bulletToShoot = typesInstances[shootNumber];
        Projectile projectileComponent = bulletToShoot.GetComponent<Projectile>();
        Rigidbody2D rb = bulletToShoot.GetComponent<Rigidbody2D>(); 

        projectileComponent.dmg = m_dmg;
        projectileComponent.range = m_range;
        projectileComponent.hitLayer = m_Layer;
        projectileComponent.owner = _owner;

        target = m_target;
        startPosition = m_startPosition;

        bulletToShoot.GetComponent<SpriteRenderer>().material.color = _color;
        bulletToShoot.transform.position = shootPosition;
        bulletToShoot.SetActive(true);

        rb.AddForce(Direction() * m_speed, ForceMode2D.Impulse);


    }

    private Vector2 Direction()
    {
        Vector2 direction = (target - startPosition).normalized;

        return direction;
    }

}
