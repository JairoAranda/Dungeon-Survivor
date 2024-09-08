using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodParticlePool : GeneralPool
{
    int bloodNumber = -1;

    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += SpawnBlood;

        PlayerStats.EventTriggerDeathPlayer += SpawnBlood;

        EnemyStats.EventTriggerHitEnemy += SpawnBlood;

        EnemyStats.EventTriggerDeathEnemy += SpawnBlood;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= SpawnBlood;

        PlayerStats.EventTriggerDeathPlayer -= SpawnBlood;

        EnemyStats.EventTriggerHitEnemy -= SpawnBlood;

        EnemyStats.EventTriggerDeathEnemy -= SpawnBlood;
    }

    void SpawnBlood(GameObject go)
    {
        bloodNumber++;

        if (bloodNumber > poolSize - 1)
        {
            bloodNumber = 0;
        }

        GameObject blood = typesInstances[bloodNumber];

        blood.transform.position = go.transform.position;

        blood.GetComponent<ParticleSystem>().Play();
    }
}
