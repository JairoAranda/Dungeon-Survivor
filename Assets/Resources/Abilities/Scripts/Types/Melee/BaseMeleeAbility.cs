using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeleeAbility : BaseAbility
{
    private Transform _armPosition;

    public Transform armPosition
    {
        get => _armPosition;
        set => _armPosition = value;
    }

    private MeleeDmg _meleeDmg;

    public MeleeDmg meleeDmg
    {
        get => _meleeDmg;
        set => _meleeDmg = value;
    }

    private HandMovement _handMovement;

    public HandMovement handMovement
    {
        get => _handMovement;
        set => _handMovement = value;
    }

    protected virtual void OnEnable()
    {
        armPosition = MeleeHitController.instance.armPosition.transform;

        handMovement = MeleeHitController.instance.handMovement;

    }

    protected virtual void Atributes()
    {
        meleeDmg = MeleeHitController.instance.meleeDmg;

        BoxCollider2D boxCollider2D = meleeDmg.gameObject.GetComponent<BoxCollider2D>();

        boxCollider2D.enabled = false;

        boxCollider2D.enabled = true;

        handMovement.enabled = false;

        meleeDmg.canDmg = true;

        meleeDmg.effectType = MeleeHitController.instance.effect;

        meleeDmg.dmg = PlayerStats.instance.dmg;

        MeleeHitController.instance.candHit = false;
    }
}
