using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyEnable))]
public class EnemyPoolManager : GeneralPool
{
    private EnemyEnable enemyEnable;

    protected override void Start()
    {
        // Obtiene el componente EnemyEnable asociado a este GameObject
        enemyEnable = GetComponent<EnemyEnable>();

        base.Start();

        // Inicializa el sistema de generación de enemigos
        enemyEnable.LateStart();
    }
}
