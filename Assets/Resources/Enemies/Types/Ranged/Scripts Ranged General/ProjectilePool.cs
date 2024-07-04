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
            bullets[i] = Instantiate(bullet, new Vector2(0, 0f), Quaternion.identity);
        }
    }
    public void ShootBullet(Vector2 shootPosition, float m_speed, float m_dmg, Transform m_target, string m_tag)
    {
        shootNumber++;

        if (shootNumber > bulletPoolSize - 1)
        {
            shootNumber = 0;
        }

        bullets[shootNumber].GetComponent<Projectile>().speed = m_speed;
        bullets[shootNumber].GetComponent<Projectile>().dmg = m_dmg;
        bullets[shootNumber].GetComponent<Projectile>().target = m_target;
        bullets[shootNumber].GetComponent<Projectile>().hitTag = m_tag;
        bullets[shootNumber].transform.position = shootPosition;
        bullets[shootNumber].SetActive(true);
    }

}
