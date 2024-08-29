using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyEnable))]
public class EnemyPoolManager : GeneralPool
{
    private EnemyEnable enemyEnable;

    protected override void Start()
    {
        enemyEnable = GetComponent<EnemyEnable>();

        base.Start();

        enemyEnable.LateStart();

    }

}
