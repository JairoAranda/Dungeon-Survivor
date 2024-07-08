using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyStats : MonoBehaviour
{
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
    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
