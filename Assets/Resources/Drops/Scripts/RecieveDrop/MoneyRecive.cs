using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRecive : GeneralReciveDrop
{
    protected override void AnimDone()
    {
        base.AnimDone();

        MoneyManager.money += Mathf.RoundToInt(totalValue);

    }
}
