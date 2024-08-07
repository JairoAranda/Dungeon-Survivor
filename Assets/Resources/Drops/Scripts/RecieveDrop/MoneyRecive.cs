using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRecive : GeneralReciveDrop
{
    [Range(0f, 10f)]
    [SerializeField] float baseMoney = 5;
    protected override void AnimDone()
    {
        float _money = XPCalc(sOPlayerInfo.money, baseMoney, 2f);

        MoneyManager.instance.money += Mathf.RoundToInt(_money);

        base.AnimDone();

    }
}
