using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    float life {get; set;}

    void GetHit(float dmg);
}