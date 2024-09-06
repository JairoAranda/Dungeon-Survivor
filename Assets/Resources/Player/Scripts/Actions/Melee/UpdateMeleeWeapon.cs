using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMeleeWeapon : MonoBehaviour
{
    IEffectType _effect;

    MeleeDmg _meleeDmg;

    private void Start()
    {
        _effect = GetComponent<IEffectType>();

        MeleeHitController.instance.effect = _effect;

        _meleeDmg = GetComponent<MeleeDmg>();

        MeleeHitController.instance.meleeDmg = _meleeDmg;
    }

}
