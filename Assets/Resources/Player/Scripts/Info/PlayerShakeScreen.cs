using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShakeScreen : ShakeScreen
{
    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += TriggerShake;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= TriggerShake;
    }

}
