using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyStats : MonoBehaviour, IStats
{
    public static event Action<Vector3> EventTriggerHitEnemy, EventTriggerDeathEnemy;

    private SOEnemyInfo enemyInfoSO;

    private bool _isDead = false;

    public bool isDead
    {
        get => _isDead;
    }

    private float _life;

    public float life
    {
        get => _life;
        set 
        {
            _life = value;

            if (_life <= 0)
            {
                Death();
            }
        }
    }


    private void OnEnable()
    {
        if (enemyInfoSO == null)
        {
            enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;
        }

        _isDead = false;

        life = enemyInfoSO.health;
    }


    public void GetHit(float dmg)
    {
        life -= dmg;
        
        if (life > 0)
        {
            EventTriggerHitEnemy?.Invoke(transform.position);
        }

    }

    public void Death()
    {
        if (!_isDead)
        {
            _isDead = true;
            EventTriggerDeathEnemy?.Invoke(transform.position);
            gameObject.SetActive(false);
        }
        
    }
}
