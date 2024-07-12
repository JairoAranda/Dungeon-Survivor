using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    [Header("Object Pool Data")]
    [Range(1, 50)]
    [SerializeField] int bulletPoolSize = 13;
    [SerializeField] GameObject bullet;

    private GameObject[] bullets;
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
    void Start()
    {
        bullets = new GameObject[bulletPoolSize];

        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets[i] = Instantiate(bullet, Vector2.zero, Quaternion.identity);
            bullets[i].transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);
        }
    }
    public void ShootBullet(Vector2 shootPosition, float m_speed, float m_dmg, Vector3 m_target, string m_tag, bool m_lifeTime, float m_timeToDie)
    {
        shootNumber++;

        if (shootNumber > bulletPoolSize - 1)
        {
            shootNumber = 0;
        }

        GameObject bulletToShoot = bullets[shootNumber];
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
