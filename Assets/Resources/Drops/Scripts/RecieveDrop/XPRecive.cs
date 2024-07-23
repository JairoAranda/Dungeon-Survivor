using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPRecive : GeneralReciveDrop
{
    protected override void AnimDone()
    {
        base.AnimDone();

        Debug.Log("XP");
    }
}
