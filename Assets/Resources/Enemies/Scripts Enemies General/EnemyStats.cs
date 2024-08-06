using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyStats : MonoBehaviour, IStats
{
    public static event Action<Vector3> EventTriggerHitEnemy, EventTriggerDeathEnemy;

    private SOEnemyInfo enemyInfoSO;

    public float life
    {
        get => enemyInfoSO.health;
        set => enemyInfoSO.health = value;
    }

    void Start()
    {
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;

    }

    private void Update()
    {
        if (life <= 0)
        {
            Death();
        }
    }

    public void GetHit(float dmg)
    {
        life -= dmg;
        
        if (life > 0)
        {
            //EventTriggerHitEnemy();
        }

    }

    void Death()
    {
        EventTriggerDeathEnemy(gameObject.transform.position);
        gameObject.SetActive(false);
    }
}
