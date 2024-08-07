using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyEnable))]
public class EnemyPoolManager : GeneralPool
{
    public static EnemyPoolManager instance;

    private EnemyEnable enemyEnable;

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
    protected override void Start()
    {
        enemyEnable = GetComponent<EnemyEnable>();

        base.Start();

        enemyEnable.LateStart();

    }

}
