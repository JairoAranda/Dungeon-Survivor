using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShakeScreen : ShakeScreen
{
    private void OnEnable()
    {
        EnemyStats.EventTriggerHitEnemy += TriggerShake;
        EnemyStats.EventTriggerDeathEnemy += TriggerShake;
    }

    private void OnDisable()
    {
        EnemyStats.EventTriggerHitEnemy -= TriggerShake;
        EnemyStats.EventTriggerDeathEnemy -= TriggerShake;
    }
}
