using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootSound : SoundActive
{
    private void OnEnable()
    {
        EnemyShoot.EventTriggerEnemyShoot += SoundEnable;
    }

    private void OnDisable()
    {
        EnemyShoot.EventTriggerEnemyShoot -= SoundEnable;
    }
}
