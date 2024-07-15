using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyStats : MonoBehaviour
{
    public static event Action<Vector3> EventTriggerHitEnemy, EventTriggerDeathEnemy;

    private SOEnemyInfo enemyInfoSO;

    private float life;

    void Start()
    {
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;

        life = enemyInfoSO.health;
    }

    public void GetHit(float dmg)
    {
        life -= dmg;

        if (life <= 0 )
        {
            Death();
        }
        else
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
