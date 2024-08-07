using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    float life {get; set;}
    bool isDead { get;}
    void GetHit(float dmg);

    void Death();
}
