using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPRecive : GeneralReciveDrop
{

    protected override void AnimDone()
    {
        base.AnimDone();

        // A�ade el valor total de experiencia al XP del PlayerStats
        PlayerStats.instance.xp += totalValue;
    }

}
