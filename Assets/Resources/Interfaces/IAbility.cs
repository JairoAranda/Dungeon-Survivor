using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    SOPlayerInfo sOPlayerInfo { get; set; }

    ProjectilePool projectilePool { get; set; }

    float cd { get; set; }

    float currentCD { get; set; }

    void Ability();
}
