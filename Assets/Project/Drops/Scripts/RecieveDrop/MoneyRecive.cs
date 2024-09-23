using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRecive : GeneralReciveDrop
{
    protected override void AnimDone()
    {
        base.AnimDone();

        // Añade el valor total al dinero del MoneyManager
        MoneyManager.instance.money += Mathf.RoundToInt(totalValue);

    }
}
