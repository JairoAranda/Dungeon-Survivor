using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRecive : GeneralReciveDrop
{
    protected override void AnimDone()
    {
        base.AnimDone();

        MoneyManager.instance.money += Mathf.RoundToInt(totalValue);

    }
}
