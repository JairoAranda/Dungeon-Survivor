using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemySound : SoundActive
{
    private void OnEnable()
    {
        EnemyStats.EventTriggerHitEnemy += SoundEnable;
        EnemyStats.EventTriggerDeathEnemy += SoundEnable;
    }

    private void OnDisable()
    {
        EnemyStats.EventTriggerHitEnemy -= SoundEnable;
        EnemyStats.EventTriggerDeathEnemy -= SoundEnable;
    }
}
