using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPRecive : GeneralReciveDrop
{
    [Header("Orb Value")]
    [Range(0f, 10f)]
    [SerializeField] float baseXP = 5;

    [Header("Lvl Multiplier")]
    [Range(2f, 10f)]
    [SerializeField] private int multiplier = 5;

    [Header("Stat Upgrade")]
    [SerializeField] private PlayerUpgradeEnum xpUpgrade;

    protected override void AnimDone()
    {
        float _xp = baseXP * ScaleMultiplier.scaleFactor(multiplier, sOPlayerInfo.statUpgrades[xpUpgrade]);

        PlayerStats.instance.xp += _xp;

        base.AnimDone();
    }

}
