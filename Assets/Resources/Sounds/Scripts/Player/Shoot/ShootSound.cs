using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSound : SoundActive
{

    private void OnEnable()
    {
        ShootController.EventTriggerShoot += SoundEnable;
        MeleeHitController.EventTriggerSwing += SoundEnable;
    }

    private void OnDisable()
    {
        ShootController.EventTriggerShoot -= SoundEnable;
        MeleeHitController.EventTriggerSwing -= SoundEnable;
    }


}