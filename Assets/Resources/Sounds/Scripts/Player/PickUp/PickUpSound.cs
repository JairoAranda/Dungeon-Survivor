using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : SoundActive
{
    private void OnEnable()
    {
        GeneralReciveDrop.EventTriggerGetItem += SoundEnable;
    }

    private void OnDisable()
    {
        GeneralReciveDrop.EventTriggerGetItem -= SoundEnable;
    }
}

