using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrop", menuName = "Drops/Drop")]
public class SODrop : ScriptableObject
{
    [Range(0f, 10f)]
    public int minDrop, maxDrop;

    public EnumDropType types;
}
