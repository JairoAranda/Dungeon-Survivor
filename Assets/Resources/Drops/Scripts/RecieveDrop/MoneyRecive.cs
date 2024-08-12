using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRecive : GeneralReciveDrop
{
    [Header("Coin Value")]
    [Range(0f, 10f)]
    [SerializeField] float baseMoney = 5;

    [Header("Lvl Multiplier")]
    [Range(2f, 10f)]
    [SerializeField] private int multiplier = 5;

    [Header("Stat Upgrade")]
    [SerializeField] private PlayerUpgradeEnum moneyUpgrade;
    protected override void AnimDone()
    {
        float _money = baseMoney * ScaleMultiplier.scaleFactor(multiplier, sOPlayerInfo.statUpgrades[moneyUpgrade]);

        MoneyManager.instance.money += Mathf.RoundToInt(_money);

        base.AnimDone();

    }
}
