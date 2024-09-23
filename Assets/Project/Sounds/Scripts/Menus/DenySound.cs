using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenySound : SoundActive
{
    private void OnEnable()
    {
        StatUpgradeManager.EventTriggerDeny += SoundEnable;
    }

    private void OnDisable()
    {
        StatUpgradeManager.EventTriggerDeny -= SoundEnable;
    }
}
