using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitSound : SoundActive
{
    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += SoundEnable;
        PlayerStats.EventTriggerDeathPlayer += SoundEnable;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= SoundEnable;
        PlayerStats.EventTriggerDeathPlayer -= SoundEnable;
    }
}
