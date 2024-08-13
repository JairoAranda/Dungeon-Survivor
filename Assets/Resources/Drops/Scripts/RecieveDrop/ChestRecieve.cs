using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRecieve : GeneralReciveDrop
{
    protected override void AnimDone()
    {
        StartCoroutine(DestoyObject());

        Debug.Log("cofre");

    }
}
