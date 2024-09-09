using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptSound : SoundActive
{
    private void OnEnable()
    {
        StatUpgradeManager.EventTriggerAccept += SoundEnable;
    }

    private void OnDisable()
    {
        StatUpgradeManager.EventTriggerAccept -= SoundEnable;
    }
}
