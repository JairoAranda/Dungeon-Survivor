using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPRecive : GeneralReciveDrop
{

    [Range(0f, 10f)]
    [SerializeField] float baseXP = 5;
   
    protected override void AnimDone()
    {
        float _xp = XPCalc(sOPlayerInfo.xp ,baseXP, 2f);

        PlayerStats.instance.xp += _xp;

        base.AnimDone();
    }

}
